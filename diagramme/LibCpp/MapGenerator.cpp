#include "MapGenerator.h"
#include <stdlib.h>
#include <stdio.h> 

int** MapGenerator::generateMap(int size) {
	int i;
	int length = size * size;
	int** map = (int**) malloc(size * sizeof(int));
	for(i=0; i<size; i++) {
        map[i] = (int*)malloc(size * sizeof(int));
	}
	if(map == NULL) {
		printf("L'allocation n'a pu être réalisée\n");
	}
	for(i=0; i<length; i++) {
		for(int j=0; j<size; j++) {
			map[i][j] = MapGenerator::randBounds(1, 6);
		}
	}
	return map;
}

int MapGenerator::randBounds(int a, int b) {
    return rand() % (b-a) + a;
}