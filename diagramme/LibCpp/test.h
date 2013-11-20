#pragma once

#define WANTDLLEXP
#ifdef WANTDLLEXP
#define DLL __declspec( dllexport )
#else
#define DLL
#endif


class test
{
public:
	DLL test(void);
	DLL ~test(void);
	DLL int run(int);
};