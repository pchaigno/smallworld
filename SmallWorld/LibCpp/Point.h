#pragma once

#include <cmath>
#include "SmallWorld.h"
#include <iostream>

class Point {

public:
	int x;
	int y;
	Point(int x, int y);
	Point();
	bool operator==(const Point& pt) const;
	bool operator<(const Point& pt) const;
	bool isValid(int size) const;
	bool isSea(Tile** map) const;
};