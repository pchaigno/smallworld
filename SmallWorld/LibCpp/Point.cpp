#include "Point.h"

/**
 * Constructor
 * @param x Abscissa
 * @param y Ordinate
 */
Point::Point(int x, int y) {
	this->x = x;
	this->y = y;
}

/**
 * Null constructor.
 */
Point::Point() {
	this->x = -1;
	this->y = -1;
}

/**
 * Compares two points.
 * @returns True if the two points are equal.
 */
bool Point::operator==(const Point& pt) const {
	return this->x == pt.x && this->y == pt.y;
}

/**
 * Compares two points.
 * Compares at first the abscissa and then the ordinates if the abscissa are equal.
 * @returns True if the second point is greater than the first.
 */
bool Point::operator<(const Point& pt) const {
	return this->x > pt.x || (this->x==pt.x && this->y>pt.y);
}

/**
 * Checks that a position is valid on the map.
 * @param size The size of the map.
 * @returns True if the point is valid on the map.
 */
bool Point::isValid(int size) const {
	if(this->x<0 || this->x>=size) {
		return false;
	}
	if(this->y<0 || this->y>=size) {
		return false;
	}
	return true;
}

/**
 * Checks that the point is a sea position.
 * @note Only to make the code more readable.
 */
bool Point::isSea(Tile** map) const {
	return map[this->x][this->y] == SEA;
}