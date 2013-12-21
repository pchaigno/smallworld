#include "MapGenerator.h"

/**
 * Generates the map.
 * Returns a matrix of int; each int represent a certain type of square.
 * @param size The size of the map.
 * @returns The map as a matrix of int.
 */
int** MapGenerator::generateMap(int size) {
	// Initialisation:
	int i, j;
	int** map = new int*[size];
	for(i=0; i<size; i++) {
        map[i] = new int[size];
	}
	if(map == NULL) {
		printf("L'allocation n'a pu être réalisée\n");
	}
	MapGenerator::initiateRand();

	do {
		// Compute the map composition:
		for(i=0; i<size; i++) {
			for(j=0; j<size; j++) {
				map[i][j] = MapGenerator::randBounds(1, 6);
			}
		}
	} while(!isConnectedGraph(map, size));
	return map;
}

/**
 * Initiate the rand function.
 */
void MapGenerator::initiateRand() {
	srand(time(NULL));
}

/**
 * Draws lot an integer.
 * @param a
 * @param b
 * @returns A random integer between a and b (not included).
 */
int MapGenerator::randBounds(int a, int b) {
    return rand() % (b-a) + a;
}

/**
 * Check if an unit can go from one point to all others of the map.
 * @param map The map.
 * @param size The size of the map.
 * @returns True if all squares of the map are accessible from one.
 */
bool MapGenerator::isConnectedGraph(int** map, int size) {
	std::map<Point, vector<Point>> graph = MapGenerator::convertToGraph(map, size);
	vector<Point> composant = MapGenerator::getConnectedComposant(graph, MapGenerator::getKeys(graph)[0]);
	return composant.size() == graph.size();
}

/**
 * Converts the map to a graph.
 * Squares are vertices if they're not sea.
 * Two squares are connected if they're adjacent (an unit can go from one to the other in one move).
 * TODO Put the number sea as a constant.
 * Note: Sea is represented by 1.
 * @param map The map randomly generated.
 * @param size The size of the map.
 * @returns The graph as a map of adjacent vertices by vertex.
 */
map<Point, vector<Point>> MapGenerator::convertToGraph(int** map, int size) {
	std::map<Point, vector<Point>> graph;
	for(int i=0; i<size; i++) {
		for(int j=0; j<size; j++) {
			if(map[i][j] != 1) {
				vector<Point> adjacents;
				for(int x=i-1; x<=i+1; x+=2) {
					if(x>=0 && x<size && map[x][j]!=1) {
						adjacents.push_back(Point(x, j));
					}
				}
				for(int y=j-1; y<=j+1; y+=2) {
					if(y>=0 && y<size && map[i][y]!=1) {
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
vector<Point> MapGenerator::getConnectedComposant(map<Point, vector<Point>> graph, Point vertex) {
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
			// Get the index for pt:
			j = -1;
			for(int i=0; i<graph.size(); i++) {
				if(vertices[i] == pt) {
					j = i;
				}
			}
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
		return MapGenerator::getKeys(graph);
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
vector<Point> MapGenerator::getKeys(map<Point, vector<Point>> graph) {
	vector<Point> keys;
	for(map<Point, vector<Point>>::iterator it = graph.begin(); it!=graph.end(); ++it) {
		keys.push_back(it->first);
	}
	return keys;
}

/**
 * @param set1 A first set of points.
 * @param set2 A second set of points.
 * @returns The first set of point minus the points from the second one.
 */
vector<Point> MapGenerator::difference(vector<Point> set1, vector<Point> set2) {
	vector<Point> result;
	for(int i=0; i<set1.size(); i++) {
		if(std::find(set2.begin(), set2.end(), set1[i]) == set2.end()) {
			result.push_back(set1[i]);
		}
	}
	return result;
}