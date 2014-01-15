#include "AdviceGenerator.h"

/**
 * Constructor
 * @param map The map.
 * @param size The size of the map.
 * @param nationPlayer1 The nation of the first player.
 * @param nationPlayer2 The nation of the second player.
 */
AdviceGenerator::AdviceGenerator(Tile** map, int size, Nation nationPlayer1, Nation nationPlayer2) {
	this->map = map;
	this->size = size;
	this->nations = (Nation*)malloc(sizeof(Nation) * 2);
	this->nations[0] = nationPlayer1;
	this->nations[1] = nationPlayer2;
}

/**
 * Compute some advice of destination for the current selected unit.
 * @param x The abscissa of the position of the selected unit.
 * @param y The ordinate of the position of the selected unit.
 * @param units The positions of all units on the map.
 * @param player The current player (first or second).
 * @results The advice of destinations.
 */
Point* AdviceGenerator::getAdvice(int x, int y, Player** units, Player player) {
	Nation nation = nations[player-1];
	// Compute the scores for each neighbour positions:
	std::map<Point, int> scores;
	int xOffset[] = {-1, 0, 0, 1};
	int yOffset[] = {0, 1, -1, 0};
	for(int i=0; i<4; i++) {
		int xCoord = xOffset[i] + x;
		int yCoord = yOffset[i] + y;
		if(xCoord>=0 && xCoord<size && yCoord>=0 && yCoord<size && map[xCoord][yCoord]!=SEA) {
			Point neighbour = Point(xCoord, yCoord);
			int score = this->getMovementScore(neighbour, nation);
			score += this->getAttackScore(neighbour, units[xCoord][yCoord], player);
			scores.insert(make_pair(neighbour, score));
		}
	}
	
	// Retrieve the three best positive destinations:
	Point* results = new Point[3];
	int nbResults = 0;
	for(int score=3; nbResults<3 && score>0; score--) {
		for(std::map<Point, int>::iterator it = scores.begin(); nbResults<3 && it!=scores.end(); ++it) {
			if(it->second==score) {
				results[nbResults] = it->first;
				nbResults++;
			}
		}
	}
	return results;
}

/**
 * Computes the score for a specific position and a specific nation.
 * @param pos The position to check.
 * @param nation The nation of the unit to advise.
 */
int AdviceGenerator::getMovementScore(Point pos, Nation nation) {
	Tile square = this->map[pos.x][pos.y];
	int scoresDwarfs[5] = {INT_MIN, 2, 0, -2, 1};
	int scoresVikings[5] = {-1, 0, -2, 0, 0};
	int scoresGaulois[5] = {INT_MIN, 0, 0, 3, -2};
	int score = 0;
	switch(nation) {
		case DWARFS:
			score = scoresDwarfs[square - 1];
		case VIKINGS:
			score = scoresVikings[square - 1];
			if(this->map[pos.x][pos.y]!=SEA && this->hasSeaNeighbour(pos)) {
			// A viking doesn't get any point if he is on the sea
			// even if he is also next to a sea tile.
				score++;
			}
		case GAULOIS:
			score = scoresGaulois[square - 1];
	}
	return score;
}

/**
 * Checks if a tile as the sea as a neighbour.
 * @param pos The position to check for sea neighbours.
 * @returns True if one of the neighbour of the position to check is sea.
 */
bool AdviceGenerator::hasSeaNeighbour(Point pos) {
	int xOffset[] = {-1, 0, 0, 1};
	int yOffset[] = {0, 1, -1, 0};
	for(int i=0; i<4; i++) {
		int x = xOffset[i] + pos.x;
		int y = yOffset[i] + pos.y;
		if(x>=0 && x<this->size && y>=0 && y<this->size) {
			if(this->map[x][y] != SEA) {
				return true;
			}
		}
	}
	return false;
}

/**
 * Computes the score for a specific position and a specific occupant.
 * @param pos The position to check.
 * @param occupant The occupant of the position to check.
 * @param player The current player (first or second).
 */
int AdviceGenerator::getAttackScore(Point pos, Player occupant, Player player) {
	if(occupant == player) {
		return INT_MIN;
	}
	return 0;
}