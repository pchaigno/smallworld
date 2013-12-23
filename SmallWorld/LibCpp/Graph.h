#pragma once

#include "Point.h"
#include "SmallWorld.h"
#include <map>
#include <vector>
#include <stdlib.h>
#include <stdio.h>

using namespace std;

class Graph {

private:
	static vector<Point> getConnectedComposant(map<Point, vector<Point>> graph, Point vertex);
	static vector<Point> getKeys(map<Point, vector<Point>> graph);
	static vector<Point> difference(vector<Point> set1, vector<Point> set2);
	static bool inArray(Point pt, vector<Point> points);

public:
	static bool isConnectedGraph(Square** map, int size);
	static int** getBestCostRouting(map<Point, vector<Point>> graph, Point* vertices);
	static map<Point, vector<Point>> convertToGraph(Square** map, int size);
};