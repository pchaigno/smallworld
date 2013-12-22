#pragma once

#include "MapGenerator.h"
#include <list>
//#pragma comment (lib, "LibCpp.lib")
using namespace System::Collections::Generic;

namespace mWrapper {

	/**
	 * Binds the C# core with the C++ library.
	 */
	public ref class Wrapper {

	public:
		/**
		 * Generates a random map.
		 * @param size The size of the map to generate.
		 * @returns A matrix of integer representing the different types of squares.
		 */
		static array<array<int>^>^ generateMapList(int size) {
			int** result = MapGenerator::generateMap(size);

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
		static array<array<int>^>^ getStartsPlayers(array<array<int>^>^ map, int size) {
			int** result = new int*[size];
			for(int i=0; i<size; i++) {
				result[i] = new int[size];
				for(int j=0; j<size; j++) {
					result[i][j] = map[i][j];
				}
			}
			int** starts = MapGenerator::placeUnits(result, size);

			array<array<int>^>^ pos = gcnew array<array<int>^>(2);
			for(int i=0; i<2; i++) {
				pos[i] = gcnew array<int>(2);
				for(int j=0; j<2; j++) {
					pos[i][j] = starts[i][j];
				}
			}
			return pos;
		}
	};
}