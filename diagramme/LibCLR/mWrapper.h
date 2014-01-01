#pragma once

#include "MapGenerator.h"
#include <list>
#pragma comment (lib, "LibCpp.lib")
using namespace System::Collections::Generic;

namespace mWrapper {
	public ref class Wrapper {
	public:
		static List<int>^ generateMapList(int a) {
			int* tab = generateMap(a); 
			int length = a * a;
			List<int>^ result = gcnew List<int>(); 
			for (int i = 0; i < length; i++) {
				result->Add(tab[i]);
			}
			return result;
		}
	};
}