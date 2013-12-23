#include "AdviceGenerator.h"

/**
 * Constructor
 * @param map The map.
 * @param size The size of the map.
 * @param nationPlayer1 The nation of the first player.
 * @param nationPlayer2 The nation of the second player.
 */
AdviceGenerator::AdviceGenerator(Square** map, int size, Nation nationPlayer1, Nation nationPlayer2) {
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
	for(int i=x-1; i<=x+1; i+=2) {
		if(i>=0 && i<size && map[i][y]!=SEA) {
			Point neighbour = Point(i, y);
			scores.insert(make_pair(neighbour, this->getScore(neighbour, nation)));
		}
	}
	for(int j=y-1; j<=y+1; j+=2) {
		if(j>=0 && j<size && map[x][j]!=SEA) {
			Point neighbour = Point(x, j);
			scores.insert(make_pair(neighbour, this->getScore(neighbour, nation)));
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
 * Get the score for a specific position and a specific nation.
 * @param pos The position to check.
 * @param nation The nation of the unit to advise.
 */
int AdviceGenerator::getScore(Point pos, Nation nation) {
	Square square = this->map[pos.x][pos.y];
	int scoresDwarfs[5] = {INT_MIN, 2, 0, -2, 1};
	int scoresVikings[5] = {-1, 0, -2, 0, 0};
	int scoresGaulois[5] = {INT_MIN, 0, 0, 3, -2};
	switch(nation) {
		case DWARFS:
			return scoresDwarfs[square - 1];
		case VIKINGS:
			// TODO Vikings get 2 if they are next to the sea.
			return scoresVikings[square - 1];
		case GAULOIS:
			return scoresGaulois[square - 1];
	}
	return 0;
}