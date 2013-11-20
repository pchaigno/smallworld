#pragma once

#include "test.h"
#pragma comment (lib, "LibCpp.lib")
using namespace System;

namespace mWrapper {
	public ref class WrapperTest {
	private:
		test* tDCw;
	public:
		WrapperTest(){ tDCw = new test(); }
		~WrapperTest(){ delete(tDCw); }
		int run(int a) {
			return tDCw->run(a);
		}
	protected:
		!WrapperTest(){ delete(tDCw); }
	};
}