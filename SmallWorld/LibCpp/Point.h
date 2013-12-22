#pragma once

#include <cmath>

class Point {

public:
	int x;
	int y;
	Point(int x, int y);
	Point();
	bool operator==(const Point& pt) const;
	bool operator<(const Point& pt) const;
};