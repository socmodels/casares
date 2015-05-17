#!/usr/bin/env python2
import os
import sys
import math

# These from <http://www.cs.ucla.edu/classes/winter04/cs131/hw/hw4.html>
# Warning: that page also says that Python mishandles NaN
large = 1e300
inf = large * large
nan = inf - inf

class Stddev: # ex Sample
    """Tracks the mean and standard deviation of a sample.
    Does not store the entire sample in memory; there is no need."""

    def __init__(self):
        self.n = 0
        self.mean = nan
        self.sum = nan

    def append(self, value):
        """Computes mean and standard deviation on-the-fly.

        Algorithm from Knuth, The Art of Computer Programming, vol. 2,
        p. 232. (Which in turn refers to B. P. Welford, Technometrics 4
        (1962), 419-420.)
        """
        self.n = self.n + 1
        if self.n == 1:
            self.mean = value
            self.sum = 0.
        else:
            newmean = self.mean + (value - self.mean)/self.n
            self.sum = self.sum + (value - self.mean) * (value - newmean)
            self.mean = newmean

    def getMean(self):
        return self.mean

    def getStddev(self):
        if self.n < 2:
            return nan
        return math.sqrt(self.sum / (self.n-1))

    def getConfidenceIncrement(self, alpha=.05):
        """Returns a confidence interval for the mean value, in
        (center, range) form.

        See Miller and Miller, John E. Freund's Mathematical Statistics,
        Sixth Edition, p. 364.

        For now, only a very limited number of values for alpha and n are
        supported, since calculating t_{\\alpha,\\nu} is non-trivial.
        """
        t_alphadiv2_nminus1 = None

        # These values from Miller, p. 582 (Table IV).
        # TODO: Find a way to compute these.
        if self.n == 5 and alpha == .05:
            t_alphadiv2_nminus1 = 2.776
        elif self.n == 10 and alpha == .05:
            t_alphadiv2_nminus1 = 2.262
        elif self.n > 30 and alpha == .05:
            t_alphadiv2_nminus1 = 1.960
        elif self.n > 30 and alpha == .20:
            t_alphadiv2_nminus1 = 1.282
        else:
            raise "ERROR: Don't know t_alphadiv2_nminus1 for n == %d, alpha == %g" \
                  % (self.n, alpha)

        mean = self.getMean()
        stddev = self.getStddev()
        return (mean, t_alphadiv2_nminus1 * stddev / math.sqrt(self.n))

