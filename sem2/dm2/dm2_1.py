import string
import random

class GraphNode(object):
	def __init__(self, name):
		self.name = name
		self.neighbors = []

	def addNeighbor(self, neighbor):
		if not neighbor in self.neighbors \
			and neighbor.name != self.name:
			self.neighbors.append(neighbor)

	def getNeighbors(self):
		return self.neighbors

	def getName(self):
		return self.name

	def __repr__(self):
		return "GraphNode {}, neighbors count {}".format(self.getName(), len (self.getNeighbors()))
		

def getCount():
	while True:
		print "Please enter total graphs count (should be a number between 1 and 27)"
		count = raw_input()
		try:
			int(count)
			count = int(count)
			if count < 2 or count>26:
				raise RuntimeError("")
			return count
		except Exception, e:
			pass


def shouldPhillManually():
	while True:
		print "Do you want phill matrix manually? (type 'yes' or 'not')"
		answer = raw_input()
		try:
			if answer.lower() == "yes" \
				or answer.lower() == "y":
				return True
			elif answer.lower() == "no" \
				or answer.lower() == "n":
				return False
			else:
				raise RuntimeError("")
		except Exception, e:
			pass


def  fillGraphList(count):
	graphs_list = []

	for i in string.ascii_uppercase[:count]:
		node = GraphNode(i)
		graphs_list.append(node)

	return graphs_list


def fillNeighborsMatrix(graphs_list, neighbors_matrix):
	for i in graphs_list:
		cur_ind = ord(i.getName())-65
		for n in i.getNeighbors():
			n_ind = ord(n.getName())-65
			neighbors_matrix[cur_ind][n_ind] = 1
			neighbors_matrix[n_ind][cur_ind] = 1

def fillDistMatrix(graphs_list,dist_mat):
	for i in graphs_list:
		fillDistMatrixByNode(i, dist_mat)

def fillDistMatrixByNode(node, dist_mat, parents = []):
	cur_ind = ord(node.getName())-65
	for i in node.getNeighbors():
		n_ind = ord(i.getName())-65
		dist_mat[cur_ind][n_ind]=1
		for p in range(len(parents)):
			par_ind = ord(parents[p].getName())-65
			if dist_mat[par_ind][n_ind] == 0 \
				or dist_mat[par_ind][n_ind] > p + 2:
					dist_mat[par_ind][n_ind] = p + 2
		new_par = parents[:]
		new_par.insert(0,node)
		fillDistMatrixByNode(i, dist_mat, new_par) 



def makeRandomGraph(graphs_list):
	for i in graphs_list[:-1]:
		max_neib = count - ord(i.getName())+65 # ord('A') == 65
		gen_neib_count = random.randint(0, max_neib)
		max_ind = count-1
		for k in range(gen_neib_count):
			min_ind = ord(i.getName())-65 if max_neib else 0
			if max_ind == min_ind:
				break
			neighbor_ind = random.randint(min_ind, max_ind)
			i.addNeighbor(graphs_list[neighbor_ind])


def fillGraphManually(graphs_list):
	valid_nodes = map(GraphNode.getName, graphs_list)
	while True:
		print "Please enter edges. For example 'AB', 'AC'. Node in pairs should be given in alphabetic order"
		print "Valid nodes are between", graphs_list[0].getName(), graphs_list[-1].getName()
		print "To end, just enter 'end'"
		answer = raw_input()
		try:
			if answer.lower() == "end":
				break
			elif len(answer) != 2:
				raise RuntimeError("")
			else:
				n1 = answer[0].upper()
				n2 = answer[1].upper()
				if not n1 in valid_nodes\
					or not n2 in valid_nodes\
					or ord(n1) >= ord(n2):
					raise RuntimeError("")
				graphs_list[ord(n1)-65].addNeighbor(graphs_list[ord(n2)-65])	
				print "{:*^60}".format("Edge '"+answer.upper()+"'' successfully added")
		except Exception, e:
			print "{:*^30}".format("")
			print "{:*^30}".format("Wrong input string, will be ignored.")
			print "{:*^30}".format("")


def printMatrix(matrix):
	m_size = len(matrix)
	head =list(string.ascii_uppercase[:m_size])
	row_format ="{:>2}" * (len(head) + 1)
	print row_format.format("", *head)
	for i in range(m_size):
		print row_format.format(head[i], *matrix[i])


if __name__ == "__main__":
	count = getCount()
	graphs_list = fillGraphList(count)

	print "List of availibale nodes", map(GraphNode.getName, graphs_list)

	if shouldPhillManually():
		fillGraphManually(graphs_list)
	else:
		makeRandomGraph(graphs_list)

	# for i in graphs_list:
	# 	print i

	neighb_mat =  []
	for i in range(count):
		neighb_mat.append([0 for x in range(count)])

	dist_mat =  []
	for i in range(count):
		dist_mat.append([0 for x in range(count)])


	p_format = "{:*^30}"
	fillNeighborsMatrix(graphs_list, neighb_mat)
	print p_format.format("Matrix of neighbors")
	printMatrix(neighb_mat)
	print "{:*^30}".format("")

	print p_format.format("Matrix of distantion")
	fillDistMatrix(graphs_list,dist_mat)
	printMatrix(dist_mat)
	print "{:*^30}".format("")
