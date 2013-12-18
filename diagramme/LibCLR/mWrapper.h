#pragma once

#include "MapGenerator.h"
#include <list>
//#pragma comment (lib, "LibCpp.lib")
using namespace System::Collections::Generic;

namespace mWrapper {

	public ref class Wrapper {

	public:
		/**
		 * Generate a random map.
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
	};
}