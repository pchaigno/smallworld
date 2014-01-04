#include "MapGenerator.h"

int main() {
	int size = 15;
    Square** map = MapGenerator::generateMap(size);
	for(int i=0; i<size; i++) {
		for(int j=0; j<size; j++) {
			printf("%d ", map[j][i]);
		}
		printf("\n");
	}
	printf("\n\n");

	/**
	 * Test the generation of the map.
	 */
	//std::map<Point, vector<Point>> graph = MapGenerator::convertToGraph(map, size);
	/*printf("Graph:\n");
	for(std::map<Point, vector<Point>>::iterator it = graph.begin(); it!=graph.end(); ++it) {
		printf("(%d, %d):\n", it->first.x, it->first.y);
		for(int i=0; i<it->second.size(); i++) {
			printf("(%d, %d) ", it->second[i].x, it->second[i].y);
		}
		printf("\n\n");
	}
	printf("\n");*/

	/*vector<Point> composant = MapGenerator::getConnectedComposant(graph, MapGenerator::getKeys(graph)[0]);
	printf("Composant (%d):\n", composant.size());
	for(int i=0; i<composant.size() && i<5; i++) {
		printf("(%d, %d)\n", composant[i].x, composant[i].y);
	}
	printf("\n\n");*/

	/*vector<Point> remaining = MapGenerator::difference(MapGenerator::getKeys(graph), composant);
	printf("Remaining (%d):\n", remaining.size());
	for(int i=0; i<remaining.size(); i++) {
		printf("(%d, %d)\n", remaining[i].x, remaining[i].y);
	}*/

	/**
	 * Test the placement of the units.
	 */
	/*Point* pos = MapGenerator::placeUnits(map, size);
	printf("(%d, %d)\n", pos[0].x, pos[0].y);
	printf("(%d, %d)\n", pos[1].x, pos[1].y);
	printf("\n");*/

	system("pause");
    return 0;
}