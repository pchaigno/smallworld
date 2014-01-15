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
	static bool inArray(Point pt, vector<Point> points);
	static int Graph::getIndex(Point pt, Point* points, int nbPoints);

public:
	static bool isConnectedGraph(Tile** map, int size);
	int** getBestCostRouting(Point* vertices);
	Graph(Tile** map, int size);
	Point* getKeysAsArray();
	vector<Point> getKeys();
	int size() const;


	map<Point, vector<Point>> succs;

	vector<Point> getConnectedComposant(Point vertex);
};