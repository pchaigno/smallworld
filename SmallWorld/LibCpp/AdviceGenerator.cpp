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
Point* AdviceGenerator::getAdvice(int x, int y, Player** units, Player player) const {
	Nation nation = nations[player-1];
	// Compute the scores for each neighbour positions:
	std::map<Point, int> scores;
	int xOffset[] = {-1, 0, 0, 1};
	int yOffset[] = {0, 1, -1, 0};
	for(int i=0; i<4; i++) {
		Point neighbour = Point(xOffset[i] + x, yOffset[i] + y);
		if(neighbour.isValid(this->size) && !neighbour.isSea(this->map)) {
			// The movement score depends on neighbour's type of tile.
			int score = this->getMovementScore(neighbour, nation);
			// The capture score depends on who owns the neighbour position.
			score += this->getCaptureScore(neighbour, units[neighbour.x][neighbour.y], player);
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
int AdviceGenerator::getMovementScore(Point pos, Nation nation) const {
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
			if(!pos.isSea(this->map) && this->hasSeaNeighbour(pos)) {
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
bool AdviceGenerator::hasSeaNeighbour(Point pos) const {
	int xOffset[] = {-1, 0, 0, 1};
	int yOffset[] = {0, 1, -1, 0};
	for(int i=0; i<4; i++) {
		Point neighbour = Point(xOffset[i] + pos.x, yOffset[i] + pos.y);
		if(neighbour.isValid(size) && !neighbour.isSea(this->map)) {
			return true;
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
int AdviceGenerator::getCaptureScore(Point pos, Player occupant, Player player) const {
	if(occupant == player) {
	// The player already owns this position.
	// He won't win more points if he moves to it.
		return INT_MIN;
	}
	// All other situations (vacant or enemy position) depends on the player strategie
	// We only want to notify him the interesting positions.
	return 0;
}