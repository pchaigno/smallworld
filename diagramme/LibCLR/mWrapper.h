#pragma once

#include "MapGenerator.h"
#include <list>
//#pragma comment (lib, "LibCpp.lib")
using namespace System::Collections::Generic;

namespace mWrapper {

	public ref class Wrapper {

	public:
		static array<array<int>^>^ generateMapList(int size) {
			array<array<int>^>^ map = gcnew array<array<int>^>(size);
			int** result = MapGenerator::generateMap(size);
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