#include "MapGenerator.h"
#include <stdlib.h>
#include <stdio.h> 

int** MapGenerator::generateMap(int size) {
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
	for(i=0; i<size; i++) {
		for(j=0; j<size; j++) {
			map[i][j] = MapGenerator::pseudoRandSquare(map, size, i, j);
		}
	}
	for(i=0; i<size; i++) {
		for(j=0; j<size; j++) {
			printf("%d ", map[i][j]);
		}
		printf("\n");
	}
	return map;
}

int MapGenerator::pseudoRandSquare(int** map, int size, int x, int y) {
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

	int randIndex = MapGenerator::randBounds(0, 12);
	if(neighbors[randIndex] == 0) {
		return randBounds(1, 6);
	}
	return neighbors[randIndex];
}

int MapGenerator::randBounds(int a, int b) {
    return rand() % (b-a) + a;
}