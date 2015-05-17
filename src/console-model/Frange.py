def frange(start, end=None, inc=None):
	"A range function, that does accept float increments..."

	if end == None:
		end = start + 0.0
		start = 0.0

	if inc == None:
		inc = 1.0

	L = []
	saltosf = abs((end-start)/inc) + .9
	saltos = int(saltosf)
	for n in xrange(saltos):
		next = start + len(L) * inc
		L.append(next)
		
	return L