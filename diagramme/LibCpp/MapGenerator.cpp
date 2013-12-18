#include "MapGenerator.h"
#include <stdlib.h>
#include <stdio.h>
#include <time.h>

/**
 * Generates the map.
 * Returns a matrix of int; each int represent a certain type of square.
 * @param size The size of the map.
 * @returns The map as a matrix of int.
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
	for(i=0; i<size; i++) {
		for(j=0; j<size; j++) {
			map[i][j] = 0;
		}
	}
	MapGenerator::initiateRand();

	// Compute the map composition:
	for(i=0; i<size; i++) {
		for(j=0; j<size; j++) {
			map[i][j] = MapGenerator::pseudoRandSquare(map, size, i, j);
		}
	}
	return map;
}

/**
 * Generate a square depending of the types of the nearby squares.
 * @param map The intermediate composition of the map.
 * @param size The size of the map.
 * @param x The abscissa of the square to generate.
 * @param y The ordinate of the square to generate.
 * @returns An integer representing the type of the square.
 */
int MapGenerator::pseudoRandSquare(int** map, int size, int x, int y) {
	// Fills an array with all the neightbor values:
	int* neighbors = new int[12];
	int k, xOffset, yOffset;
	int i[12] = {-1, 0, 0, 1, -1, -1, 1, 1, -1, 0, 0, 1};
	int j[12] = {-1, -1, -1, -1, 0, 0, 0, 0, 1, 1, 1, 1};
	for(k=0; k<12; k++) {
		xOffset = x + i[k];
		yOffset = y + i[k];
		if(xOffset>=0 && xOffset<size && yOffset>=0 && yOffset<size) {
			neighbors[k] = map[xOffset][yOffset];
		} else {
			neighbors[k] = 0;
		}
	}

	// Gets randomly an integer in the array:
	// If its 0, returns an integer between 1 and 6 (not included).
	int randIndex = MapGenerator::randBounds(0, 12);
	if(neighbors[randIndex] == 0) {
		return randBounds(1, 6);
	}
	return neighbors[randIndex];
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