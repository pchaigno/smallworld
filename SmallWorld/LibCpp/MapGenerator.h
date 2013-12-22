#pragma once

#include "Point.h"
#include <map>
#include <vector>
#include <stdlib.h>
#include <stdio.h>
#include <time.h>

#define DLL __declspec( dllimport )
#define EXTERNC extern "C"

using namespace std;

class MapGenerator {

public:
	DLL static int** generateMap(int size);
	DLL static int** placeUnits(int** map, int size);

private:
	// TODO Is DLL keyword mandatory?
	DLL static bool isConnectedGraph(int** map, int size);
	DLL static int randBounds(int a, int b);
	DLL static void initiateRand();
	DLL static vector<Point> getConnectedComposant(map<Point, vector<Point>> graph, Point vertex);
	DLL static vector<Point> MapGenerator::getKeys(map<Point, vector<Point>> graph);
	DLL static vector<Point> MapGenerator::difference(vector<Point> set1, vector<Point> set2);
	DLL static int** MapGenerator::getBestCostRouting(map<Point, vector<Point>> graph, Point* vertices);
	DLL static bool MapGenerator::inArray(Point pt, vector<Point> points);
	DLL static map<Point, vector<Point>> convertToGraph(int** map, int size);
};