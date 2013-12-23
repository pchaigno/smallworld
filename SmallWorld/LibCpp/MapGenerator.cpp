#include "MapGenerator.h"

/**
 * Places the units for both players.
 * @param map The map as a matrix of integers.
 * @param size The size of the map.
 */
Point* MapGenerator::placeUnits(Square** map, int size) {
	Point* result = new Point[2];
	result[0] = Point(0, 0);
	result[1] = Point(size-1, size-1);

	std::map<Point, vector<Point>> graph = Graph::convertToGraph(map, size);
	Point* vertices = new Point[graph.size()];
	int i=0;
	for(std::map<Point, vector<Point>>::iterator it = graph.begin(); it!=graph.end(); ++it) {
		vertices[i] = it->first;
		i++;
	}
	int** costs = Graph::getBestCostRouting(graph, vertices);
	int maxCost = 0;
	for(int i=0; i<graph.size(); i++) {
		for(int j=0; j<graph.size(); j++) {
			if(costs[i][j] > maxCost) {
				maxCost = costs[i][j];
				result[0] = vertices[i];
				result[1] = vertices[j];
			}
		}
	}

	return result;
}

/**
 * Generates the map.
 * Returns a matrix of int; each int represent a certain type of square.
 * @param size The size of the map.
 * @returns The map as a matrix of integers.
 */
Square** MapGenerator::generateMap(int size) {
	// Initialisation:
	int i, j;
	Square** map = new Square*[size];
	for(i=0; i<size; i++) {
        map[i] = new Square[size];
	}
	if(map == NULL) {
		printf("L'allocation n'a pu être réalisée\n");
	}
	MapGenerator::initiateRand();

	do {
		// Compute the map composition:
		for(i=0; i<size; i++) {
			for(j=0; j<size; j++) {
				map[i][j] = (Square)MapGenerator::randBounds(1, 6);
			}
		}
	} while(!Graph::isConnectedGraph(map, size));
	return map;
}

/**
 * Initiate the rand function.
 */
void MapGenerator::initiateRand() {
	srand(time(NULL));
}

/**
 * Draws lot an integer.
 * @param a
 * @param b
 * @returns A random integer between a and b (not included).
 */
int MapGenerator::randBounds(int a, int b) {
    return rand() % (b-a) + a;
}