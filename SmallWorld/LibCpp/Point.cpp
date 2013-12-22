#include "Point.h"

Point::Point(int x, int y) {
	this->x = x;
	this->y = y;
}

Point::Point() {
	this->x = -1;
	this->y = -1;
}

bool Point::operator==(const Point& pt) const {
	return this->x == pt.x && this->y == pt.y;
}

bool Point::operator<(const Point& pt) const {
	return this->x > pt.x || (this->x==pt.x && this->y>pt.y);
}