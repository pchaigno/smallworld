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
	map<Point, vector<Point>> succs;

	vector<Point> getConnectedComposant(const Point& vertex) const;
	static bool inArray(const Point& pt, const vector<Point>& points);
	static int Graph::getIndex(const Point& pt, Point* points, int nbPoints);

public:
	static bool isConnectedGraph(Tile** map, int size);
	int** getBestCostRouting(Point* vertices) const;
	Graph(Tile** map, int size);
	Point* getKeysAsArray() const;
	vector<Point> getKeys() const;
	int size() const;
};