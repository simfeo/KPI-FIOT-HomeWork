from Queue import *


class Field(object):

	def __init__(self, side_size, start =None, finish=None, barriers=[]):
		self._len = side_size
		self.__start = start
		self.__finish = finish
		self._barriers = barriers
		self.__field = None
		self._builded = False

	def __call__(self, *args, **kwargs):
		self._show()

	def __getitem__(self, item):
		return self.__field[item]

	def setStart(self, point):
		if point in self._barriers:
			raise RuntimeError("start point should be outside of barries")
		if point == self.__finish:
			raise RuntimeError("start point should not be equal to finish")
		if type(point) == list:
			point=tuple(point)
		self.__start = point

	def setFinish(self, point):
		if point in self._barriers:
			raise RuntimeError("finsh point should be outside of barries")
		if point == self.__start:
			raise RuntimeError("finsh point should not be equal to finish")
		if type(point) == list:
			point=tuple(point)
		self.__finish = point

	def addBarrier(self, point):
		if point in self._barriers:
			print "point already barrier"
			return
		if point == self.__start or point == self.__finish:
			raise RuntimeError("barrier point should not be equal to start or finish")
		if type(point) == list:
			point=tuple(point)
		self._barriers.append(point)

	def _build(self):
		self._builded = True
		self.__field = [[0 for i in range(self._len)] for i in range(self._len)]
		for b in self._barriers:
			self[b[0]][b[1]] = -1
		self[self.__start[0]][self.__start[1]] = 1

	def emit(self):
		if not self._builded:
			self._build()
		q = Queue()
		q.put(self.__start)
		if self.__start == None or self.__finish == None:
			raise RuntimeError("Star and finish should be defined")
		while not q.empty():
			index = q.get()
			l = (index[0]-1, index[1])
			r = (index[0]+1, index[1])
			u = (index[0], index[1]-1)
			d = (index[0], index[1]+1)

			if l[0] >= 0 and self[l[0]][l[1]] == 0:
				self[l[0]][l[1]] += self[index[0]][index[1]] + 1
				q.put(l)
			if r[0] < self._len and self[r[0]][r[1]] == 0:
				self[r[0]][r[1]] += self[index[0]][index[1]] + 1
				q.put(r)
			if u[1] >= 0 and self[u[0]][u[1]] == 0:
				self[u[0]][u[1]] += self[index[0]][index[1]] + 1
				q.put(u)
			if d[1] < self._len and self[d[0]][d[1]] == 0:
				self[d[0]][d[1]] += self[index[0]][index[1]] + 1
				q.put(d)

	def get_path(self):
		if not self._builded:
			self._build()

		if self[self.__finish[0]][self.__finish[1]] == 0 or \
				self[self.__finish[0]][self.__finish[1]] == -1:
			raise

		path = []
		item = self.__finish
		while not path.append(item) and item != self.__start:
			l = (item[0]-1, item[1])
			if l[0] >= 0 and self[l[0]][l[1]] == self[item[0]][item[1]] - 1:
				item = l
				continue
			r = (item[0]+1, item[1])
			if r[0] < self._len and self[r[0]][r[1]] == self[item[0]][item[1]] - 1:
				item = r
				continue
			u = (item[0], item[1]-1)
			if u[1] >= 0 and self[u[0]][u[1]] == self[item[0]][item[1]] - 1:
				item = u
				continue
			d = (item[0], item[1]+1)
			if d[1] < self._len and self[d[0]][d[1]] == self[item[0]][item[1]] - 1:
				item = d
				continue
		return reversed(path)

	def update(self):
		self.__field = [[0 for i in range(self._len)] for i in range(self._len)]

	def _show(self):
		if not self._builded:
			self._build()
		row_format ="{:>3}" * self.getSize()
		for i in self:
			print row_format.format(*i)
		print

	def getSize(self):
		return self._len


def getSize():
	while True:
		print "Please enter field size. Should be beetwen 5 and 60"
		count = raw_input()
		try:
			int(count)
			count = int(count)
			if count < 5 or count>60:
				raise RuntimeError("")
			return count
		except Exception, e:
			pass


def getPoint(field, point_type):
	while True:
		print "Please enter {} point. Should be two integer number separated by coma".format(point_type)
		print "beetwen 0 and {}. For example ".format(field.getSize())
		point = raw_input()
		try:
			point = point.split(",")
			if len(point) != 2:
				raise RuntimeError("")
			point = map(str.strip, point)
			point = map(int, point)

			if point[0] >=0 \
				and point[0] <field.getSize()\
				and point[1] >=0 \
				and point[1] < field.getSize():
				return point
		except Exception, e:
			print "{:*^30}".format("")
			print "{:*^30}".format("Wrong input string, will be ignored.")
			print "{:*^30}".format("")


def fillPoint(field, point_type, func, ask_exit = False):
	while True:
		try:
			func(getPoint(field, point_type))
			if ask_exit:
				while True:
					print "Do you want to end? (type 'yes' or 'not')"
					answer = raw_input()
					if answer.lower() == "yes" \
						or answer.lower() == "y":
						break
					elif answer.lower() == "no" \
						or answer.lower() == "n":
						raise RuntimeError("")
				break
			else:
				break
		except Exception, e:
			print e.message


if __name__ == '__main__':
	size = getSize()

	field = Field(side_size = size)

	fillPoint(field,"start", field.setStart)
	fillPoint(field,"finish", field.setFinish)
	fillPoint(field, "barrier", field.addBarrier, True)


	# field = Field(side_size=5, start=(0, 0), finish=(2, 4),
	# 			  barriers=[(2, 3), (3, 2), (1, 4)])
	field.emit()
	field()
	try:
		path = field.get_path()
		for p in path:
			print p,
	except:
		print 'Path not found'