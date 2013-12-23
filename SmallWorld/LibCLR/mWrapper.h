#pragma once

#include "MapGenerator.h"
#include "AdviceGenerator.h"
#include "Point.h"
#include "SmallWorld.h"
#include <list>

using namespace System::Collections::Generic;

namespace mWrapper {

	/**
	 * Binds the C# core with the C++ library.
	 */
	public ref class Wrapper {

	public:
		static array<array<int>^>^ generateMapList(int size);
		static array<array<int>^>^ getStartsPlayers(array<array<int>^>^ map, int size);
		static array<array<int>^>^ getAdvice(array<array<int>^>^ map, int size, int nationPlayer1, int nationPlayer2, int x, int y, array<array<int>^>^ units, int player);
	};
}