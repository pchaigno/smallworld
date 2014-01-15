#include "MapGenerator.h"

/**
 * Places the units for both players.
 * @param map The map as a matrix of integers.
 * @param size The size of the map.
 */
Point* MapGenerator::placeUnits(Tile** map, int size) {
	Point* result = new Point[2];
	result[0] = Point(0, 0);
	result[1] = Point(size-1, size-1);

	// Builds the graph from the map and
	// retrieves the vertices:
	Graph graph = Graph(map, size);
	Point* vertices = graph.getKeysAsArray();

	// Computes the best cost routing for each path of the graph.
	int** costs = graph.getBestCostRouting(vertices);

	// Retrieves the longest path on the graph:
	// Its ends are two farthest points.
	int maxCost = 0;
	for(int i=0; i<graph.size(); i++) {
		for(int j=0; j<graph.size(); j++) {
			if(i!=j && costs[i][j]>maxCost) {
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
Tile** MapGenerator::generateMap(int size) {
	// Initialisation:
	int i, j;
	Tile** map = new Tile*[size];
	for(i=0; i<size; i++) {
        map[i] = new Tile[size];
	}
	MapGenerator::initiateRand();

	do {
		// Compute the map composition:
		for(i=0; i<size; i++) {
			for(j=0; j<size; j++) {
				map[i][j] = (Tile)MapGenerator::randBounds(1, 6);
			}
		}
	// Try again until the graph is connected.
	// If it's connected it means that the entire map is accessible.
	} while(!Graph::isConnectedGraph(map, size));
	return map;
}

/**
 * Initiates the rand function.
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