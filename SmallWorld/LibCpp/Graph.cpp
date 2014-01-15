#include "Graph.h"

/**
 * @returns The size of the graph.
 */
int Graph::size() const {
	return this->succs.size();
}

/**
 * Checks if an unit can go from one point to all others of the map.
 * @param map The map.
 * @param size The size of the map.
 * @returns True if all squares of the map are accessible from one.
 */
bool Graph::isConnectedGraph(Tile** map, int size) {
	Graph graph = Graph(map, size);
	vector<Point> composant = graph.getConnectedComposant(graph.getKeys()[0]);
	return composant.size() == graph.size();
}

/**
 * Converts the map to a graph.
 * Squares are vertices if they're not sea.
 * Two squares are connected if they're adjacent (an unit can go from one to the other in one move).
 * Note: Sea is represented by 1.
 * @param map The map randomly generated.
 * @param size The size of the map.
 * @returns The graph as a map of adjacent vertices by vertex.
 */
Graph::Graph(Tile** map, int size) {
	for(int i=0; i<size; i++) {
		for(int j=0; j<size; j++) {
			Point pos = Point(i, j);
			if(!pos.isSea(map)) {
				vector<Point> adjacents;
				Point adjacent;
				for(int x=i-1; x<=i+1; x+=2) {
					adjacent = Point(x, j);
					if(x>=0 && x<size && !adjacent.isSea(map)) {
						adjacents.push_back(adjacent);
					}
				}
				for(int y=j-1; y<=j+1; y+=2) {
					adjacent = Point(i, y);
					if(y>=0 && y<size && !adjacent.isSea(map)) {
						adjacents.push_back(adjacent);
					}
				}
				this->succs.insert(make_pair(pos, adjacents));
			}
		}
	}
}

/**
 * Tarjan algorithm to find a connected composant from a starting vertex.
 * @param vertex The vertex to start.
 * @returns The connected composant containing vertex.
 */
vector<Point> Graph::getConnectedComposant(const Point& vertex) const {
	// Attributes integers to each vertex:
	Point* vertices = new Point[this->size()];
	int j=0;
	for(map<Point, vector<Point>>::const_iterator it = this->succs.begin(); it!=this->succs.end(); ++it) {
		vertices[j] = it->first;
		j++;
	}
	
	// Initialization:
	int* p = new int[this->succs.size()];
	int* d = new int[this->succs.size()];
	int* n = new int[this->succs.size()];
	int* num = new int[this->succs.size()];
	int numA = -1;
	for(int i=0; i<this->succs.size(); i++) {
		num[i] = -1;
		p[i] = -1;
		d[i] = this->succs.at(vertices[i]).size();
		n[i] = -1;
		if(vertices[i] == vertex) {
			numA = i;
		}
	}
	int index = numA;
	int k = 0;
	num[numA] = 0;
	p[numA] = numA;

	// Core of the algorithm:
	while(n[index]+1!=d[index] || index!=numA) {
		if(n[index]+1 == d[index]) {
			index = p[index];
		} else {
			n[index]++;
			Point pt = this->succs.at(vertices[index])[n[index]];
			j = Graph::getIndex(pt, vertices, this->size());
			if(p[j] == -1) {
				p[j] = index;
				index = j;
				k++;
				num[index] = k;
			}
		}
	}

	// Check if the graph is connected:
	if(k+1 == this->size()) {
		return this->getKeys();
	}

	vector<Point> connectedVertices;
	for(int i=0; i<this->size(); i++) {
		if(num[i]<=k && num[i]!=-1) {
			connectedVertices.push_back(vertices[i]);
		}
	}
	return connectedVertices;
}

/**
 * @returns The keys of the graph object.
 */
vector<Point> Graph::getKeys() const {
	vector<Point> keys;
	for(map<Point, vector<Point>>::const_iterator it = this->succs.begin(); it!=this->succs.end(); ++it) {
		keys.push_back(it->first);
	}
	return keys;
}

/**
 * @returns The keys of the graph object.
 */
Point* Graph::getKeysAsArray() const {
	Point* keys = new Point[this->size()];
	int i = 0;
	for(map<Point, vector<Point>>::const_iterator it = this->succs.begin(); it!=this->succs.end(); ++it) {
		keys[i] = it->first;
		i++;
	}
	return keys;
}

/**
 * Roy-Marshall algorithm to find the best cost routing in a graph.
 * @param vertices The vertices as an array (to associate a Point to a number).
 * @returns The matrix with the best cost for each origin-destination couple.
 */
int** Graph::getBestCostRouting(Point* vertices) const {
	// Initialization:
	int** routes = new int*[this->size()];
	int** costs = new int*[this->size()];
	for(int i=0; i<this->size(); i++) {
		routes[i] = new int[this->size()];
		costs[i] = new int[this->size()];
		for(int j=0; j<this->size(); j++) {
			if(inArray(vertices[j], this->succs.at(vertices[i]))) {
				routes[i][j] = j;
				costs[i][j] = 1;
			} else {
				routes[i][j] = -1;
				costs[i][j] = INT_MAX;
			}
		}
	}

	// Roy-Marshall's algorithm:
	for(int i=0; i<this->size(); i++) {
		for(int x=0; x<this->size(); x++) {
			if(routes[x][i] != -1) {
				for(int y=0; y<this->size(); y++) {
					if(routes[i][y] != -1) {
						if(costs[x][y] > costs[x][i]+costs[i][y]) {
							costs[x][y] = costs[x][i] + costs[i][y];
							routes[x][y] = routes[x][i];
						}
					}
				}
			}
		}
	}

	return costs;
}

/**
 * Checks if a point if in a vector of points.
 * @param pt The point.
 * @param points The vector of points.
 * @returns True if pt is in points.
 */
bool Graph::inArray(const Point& pt, const vector<Point>& points) {
	for(int i=0; i<points.size(); i++) {
		if(pt == points[i]) {
			return true;
		}
	}
	return false;
}

/**
 * Checks if a point if in a vector of points.
 * @param pt The point.
 * @param points The array of points.
 * @param nbPoints The number of points in the array.
 * @returns The index of pt in points or -1 if pt is not in points.
 */
int Graph::getIndex(const Point& pt, Point* points, int nbPoints) {
	for(int i=0; i<nbPoints; i++) {
		if(pt == points[i]) {
			return i;
		}
	}
	return -1;
}