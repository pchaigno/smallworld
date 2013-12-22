#pragma once

#include "Point.h"
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
	static bool isConnectedGraph(int** map, int size);
	static map<Point, vector<Point>> convertToGraph(int** map, int size);
	static int** getBestCostRouting(map<Point, vector<Point>> graph, Point* vertices);
};