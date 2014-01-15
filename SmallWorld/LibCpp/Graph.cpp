#include "Graph.h"

/**
 * Checks if an unit can go from one point to all others of the map.
 * @param map The map.
 * @param size The size of the map.
 * @returns True if all squares of the map are accessible from one.
 */
bool Graph::isConnectedGraph(Tile** map, int size) {
	std::map<Point, vector<Point>> graph = Graph::convertToGraph(map, size);
	vector<Point> composant = Graph::getConnectedComposant(graph, Graph::getKeys(graph)[0]);
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
map<Point, vector<Point>> Graph::convertToGraph(Tile** map, int size) {
	std::map<Point, vector<Point>> graph;
	for(int i=0; i<size; i++) {
		for(int j=0; j<size; j++) {
			if(map[i][j] != SEA) {
				vector<Point> adjacents;
				for(int x=i-1; x<=i+1; x+=2) {
					if(x>=0 && x<size && map[x][j]!=SEA) {
						adjacents.push_back(Point(x, j));
					}
				}
				for(int y=j-1; y<=j+1; y+=2) {
					if(y>=0 && y<size && map[i][y]!=SEA) {
						adjacents.push_back(Point(i, y));
					}
				}
				graph.insert(make_pair(Point(i, j), adjacents));
			}
		}
	}
	return graph;
}

/**
 * Tarjan algorithm to find a connected composant.
 * @param graph The graph as a map of adjacent vertices by vertex.
 * @param vertex The vertex to start
 * @returns The connected composant containing vertex.
 */
vector<Point> Graph::getConnectedComposant(map<Point, vector<Point>> graph, Point vertex) {
	// Attributes integers to each vertex:
	Point* vertices = new Point[graph.size()];
	int j=0;
	for(map<Point, vector<Point>>::iterator it = graph.begin(); it!=graph.end(); ++it) {
		vertices[j] = it->first;
		j++;
	}
	
	// Initialization:
	int* p = new int[graph.size()];
	int* d = new int[graph.size()];
	int* n = new int[graph.size()];
	int* num = new int[graph.size()];
	int numA = -1;
	for(int i=0; i<graph.size(); i++) {
		num[i] = -1;
		p[i] = -1;
		d[i] = graph[vertices[i]].size();
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
			Point pt = graph[vertices[index]][n[index]];
			j = Graph::getIndex(pt, vertices, graph.size());
			if(p[j] == -1) {
				p[j] = index;
				index = j;
				k++;
				num[index] = k;
			}
		}
	}

	// Check if the graph is connected:
	if(k+1 == graph.size()) {
		return Graph::getKeys(graph);
	}

	vector<Point> connectedVertices;
	for(int i=0; i<graph.size(); i++) {
		if(num[i]<=k && num[i]!=-1) {
			connectedVertices.push_back(vertices[i]);
		}
	}
	return connectedVertices;
}

/**
 * @param graph A graph.
 * @param The keys of the graph object.
 */
vector<Point> Graph::getKeys(map<Point, vector<Point>> graph) {
	vector<Point> keys;
	for(map<Point, vector<Point>>::iterator it = graph.begin(); it!=graph.end(); ++it) {
		keys.push_back(it->first);
	}
	return keys;
}

/**
 * Roy-Marshall algorithm to find the best cost routing in a graph.
 * @param graph The graph.
 * @param vertices The vertices as an array (to associate a Point to a number).
 * @returns The matrix with the best cost for each origin-destination couple.
 */
int** Graph::getBestCostRouting(map<Point, vector<Point>> graph, Point* vertices) {
	// Initialization:
	int** routes = new int*[graph.size()];
	int** costs = new int*[graph.size()];
	for(int i=0; i<graph.size(); i++) {
		routes[i] = new int[graph.size()];
		costs[i] = new int[graph.size()];
		for(int j=0; j<graph.size(); j++) {
			if(inArray(vertices[j], graph[vertices[i]])) {
				routes[i][j] = j;
				costs[i][j] = 1;
			} else {
				routes[i][j] = -1;
				costs[i][j] = INT_MAX;
			}
		}
	}

	// Roy-Marshall's algorithm:
	for(int i=0; i<graph.size(); i++) {
		for(int x=0; x<graph.size(); x++) {
			if(routes[x][i] != -1) {
				for(int y=0; y<graph.size(); y++) {
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
bool Graph::inArray(Point pt, vector<Point> points) {
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
int Graph::getIndex(Point pt, Point* points, int nbPoints) {
	for(int i=0; i<nbPoints; i++) {
		if(pt == points[i]) {
			return i;
		}
	}
	return -1;
}