#include "MapGenerator.h"

/**
 * Places the units for both players.
 * @param map The map as a matrix of integers.
 * @param size The size of the map.
 */
int** MapGenerator::placeUnits(int** map, int size) {
	int** result = new int*[2];
	result[0] = new int[2];
	result[1] = new int[2];
	result[0][0] = 0;
	result[0][1] = 0;
	result[1][0] = size - 1;
	result[1][1] = size - 1;

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
				Point ptA = vertices[i];
				result[0][0] = ptA.x;
				result[0][1] = ptA.y;
				Point ptB = vertices[j];
				result[1][0] = ptB.x;
				result[1][1] = ptB.y;
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
int** MapGenerator::generateMap(int size) {
	// Initialisation:
	int i, j;
	int** map = new int*[size];
	for(i=0; i<size; i++) {
        map[i] = new int[size];
	}
	if(map == NULL) {
		printf("L'allocation n'a pu être réalisée\n");
	}
	MapGenerator::initiateRand();

	do {
		// Compute the map composition:
		for(i=0; i<size; i++) {
			for(j=0; j<size; j++) {
				map[i][j] = MapGenerator::randBounds(1, 6);
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