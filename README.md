# Casares Labor Market models

The Casares multiagent simulation model is conceived to test and try dynamics of labor
market dynamics.

The main concern introduced in this model is to explore the relevance of the decay on the
reservation wage during unemployment to the overall market. During past research about
unemployment effects in the area of Buenos Aires (Persia & De Grande, 2000), the variation
on the reservation wage, along with the discouragement effect toward stay looking for a job
under hostile conditions, appeared as major factors in jeopardizing the chances of reentrance
to the labor market.

This stylized model shows plausible relations among aggregated variables of labor market
dynamics (participation rate, unemployment, income, salary). This version allows visualizing
the flows and socioeconomic changes of the population, as well as interactively modifying the
model parameters during the simulation.

# About the model
There are two kinds of agents in the Casares model: persons and companies. The interaction
between these agents if governed by the following constrains:

1) Persons can be at three different employment states: occupied, unemployed or inactive. At
inactive state, they stay out of the labor market (i.e. they do not have work and do not look for
a job either).
2) While being unemployed, they look for a job. They can perform a new interview every
month in order to find a company to work at.
3) Both companies and persons have a fixed color tag, from 5 different possible values.
Employees with color tag A cannot work at companies with color tags B, C, D or E.
4) The recruitment interviews follow two criterions:
- The company must match with the person’s color tag in order to hire the employee.
Every company and every person hold a color tag that acts as a metaphor for specific
preferences of the company and/or employee.
- If the employee’s color matches the company’s color, the company will hire the
employee setting her current salary equal to its reservation wage (the minimum amount of
money she is able to accept). It will not try to lower such threshold, nor will pay any more
money than that (i.e. in the model, the candidates are transparent regarding its reservation
wage).
5) As an unemployed person keeps looking for a job (sometimes not having being lucky at
finding one that fits is color), she can act in two different manners:
- Stay looking for jobs.
- Give up.
6) The act of ‘giving up’ because someone believes there won’t be a place for her turns that
person into an ‘inactive by discouragement’. The time a person waits until she gives up
looking for a job is one of the main parameters of the model. However, no all the people can
chose not to work. At the default configuration of the model, the population is made of 4000
persons, where 1000 are single are 3000 are married (making 1500 couples). This is relevant
when it comes to participation in the market, as only the persons having a partner who is
occupied can chose no to look for job.
7) When the person keeps looking for jobs, the most direct way (from her perspective) to get a
job as soon as possible is reducing her reservation wage. Even when the reservation wage can
be seen as ‘what a person needs to survive’, under scenarios of downward social mobility
reservation wage can lowered significantly. The rate at which the unemployed population
lowers its reservation wage is also a parameter of the model.
8) For the simulated companies, the profit is a major concern. As the model is focus in
modeling the behavior of unemployment, the measure for profit is implemented in a very
simplistic way: every employee has a fixed productivity over time (20 credits). The cost for
production for the companies is exclusively the salaries. Such salaries grow over time due to
seniority regulations. The seniority at the model represents the bonus for being long time at
the company, but it also represents other aspects of production that may lead a company to
situations where productivity cannot keep the track of salaries: costs of obsolescence of core
technologies, effects of new competitors, changes on preferences of the demand for its
products, etc.
9) At the end of every year, companies consolidate their balances and they can decide to fire
people before the salaries of old employees rise too high. The procedure for doing such thing
makes use of the two parameters:
- Profit baseline (profit): the amount of profit (measured as the % of total product)
under which the company will decide to fire people.
- Maximum dismissal rate (dismissal rate): the quota of the personnel that companies
fire when they have no profit at all. The actual quota of people a company will decide to fire
after it’s end-of-the-year balance will be cero if the profits are higher than the profit baseline
and in a linear relation between zero (for profits = profit baseline) and the dismissal rate (for
profit = 0).
10) When companies cannot prevent themselves to have ‘negative profits’, they go into
bankruptcy (they become ‘inactive’). After a period of latency, new companies can be created
where old companies have gone into bankruptcy.

# Versions available

The repository hosts two implementations of the model:
- console-model: a Python working implementation of the rules and actores of the model.
- interactive-simulator: a C-sharp working implementation of the rules and actores of the model that allows for interactive (visual) test of the dynamics of the model.

# Model configuration

The town.ini and run.ini can be used to configure the execution of the model. While town.ini can be used to set the demographics of the simulated town (e.g. # of persons), run.ini is used to configure the behaviour of the model.
