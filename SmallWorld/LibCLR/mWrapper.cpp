// Il s'agit du fichier DLL principal.

#include "mWrapper.h"

/**
 * Generates a random map.
 * @param size The size of the map to generate.
 * @returns A matrix of integer representing the different types of squares.
 */
array<array<int>^>^ mWrapper::Wrapper::generateMapList(int size) {
	Tile** result = MapGenerator::generateMap(size);

	// Convert the result from the C++ library into a matrix readable by C#:
	array<array<int>^>^ map = gcnew array<array<int>^>(size);
	for(int i=0; i<size; i++) {
		map[i] = gcnew array<int>(size);
		for(int j=0; j<size; j++) {
			map[i][j] = result[i][j];
		}
	}
	return map;
}

/**
 * Places units of the two player on the map.
 * @param map The map as a matrix of integer.
 * @param size The size of the map.
 * @returns A matrix with the coordinate of the units for the two players.
 */
array<array<int>^>^ mWrapper::Wrapper::getStartsPlayers(array<array<int>^>^ map, int size) {
	Tile** result = new Tile*[size];
	for(int i=0; i<size; i++) {
		result[i] = new Tile[size];
		for(int j=0; j<size; j++) {
			result[i][j] = (Tile)map[i][j];
		}
	}
	Point* starts = MapGenerator::placeUnits(result, size);

	array<array<int>^>^ pos = gcnew array<array<int>^>(2);
	for(int i=0; i<2; i++) {
		pos[i] = gcnew array<int>(2);
		pos[i][0] = starts[i].x;
		pos[i][1] = starts[i].y;
	}
	return pos;
}

/**
 * Compute some advice.
 * @param map The map.
 * @param size The size of the map.
 * @param nationPlayer1 The nation of the first player.
 * @param nationPlayer2 The nation of the second player.
 * @param x The abscissa of the unit to advice.
 * @param y The ordinate of the unit to advice.
 * @param units The positions of all units on the map.
 * @param player The number of the current player.
 * @returns Some advice of destinations for the current unit.
 */
array<array<int>^>^ mWrapper::Wrapper::getAdvice(array<array<int>^>^ map, int size, int nationPlayer1, int nationPlayer2, int x, int y, array<array<int>^>^ units, int player) {
	Tile** mapBis = new Tile*[size];
	for(int i=0; i<size; i++) {
		mapBis[i] = new Tile[size];
		for(int j=0; j<size; j++) {
			mapBis[i][j] = (Tile)map[i][j];
		}
	}
	AdviceGenerator generator = AdviceGenerator(mapBis, size, (Nation)nationPlayer1, (Nation)nationPlayer2);

	Player** unitsBis = new Player*[size];
	for(int i=0; i<size; i++) {
		unitsBis[i] = new Player[size];
		for(int j=0; j<size; j++) {
			unitsBis[i][j] = (Player)units[i][j];
		}
	}
	Point* advice = generator.getAdvice(x, y, unitsBis, (Player)player);

	array<array<int>^>^ result = gcnew array<array<int>^>(3);
	for(int i=0; i<3; i++) {
		result[i] = gcnew array<int>(3);
		result[i][0] = advice[i].x;
		result[i][1] = advice[i].y;
	}
	return result;
}