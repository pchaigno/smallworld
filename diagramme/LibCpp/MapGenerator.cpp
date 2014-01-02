#include "MapGenerator.h"
#include <stdlib.h>
#include <stdio.h> 

MapGenerator::MapGenerator(int size) {
	this->size = size;
}


int* MapGenerator::generateMap(void) {
	int length = size * size;
	int * result = (int*) malloc(length * sizeof(int)); //Allocation de 20 octets
	if(result == NULL) {
		printf("L'allocation n'a pu être réalisée\n");
	}
	for (int i = 0; i <length; i++) {
		result[i] = i;
	}
	return result;
}

int* generateMap(int size) {
	MapGenerator mapGen = MapGenerator(size);
	return mapGen.generateMap();
}