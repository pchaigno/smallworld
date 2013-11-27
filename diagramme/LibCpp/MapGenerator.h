#pragma once


#ifdef WANTDLLEXP
#define DLL __declspec( dllexport )
#define EXTERNC extern "C"
#else
#define DLL __declspec( dllimport )
#define EXTERNC extern "C"
#endif



class MapGenerator
{
private:
	int size;

public:
	MapGenerator(int);
	int* generateMap(void);
};


EXTERNC DLL int* generateMap(int size);