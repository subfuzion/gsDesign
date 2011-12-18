# This R script was created via an export of a group sequential design
# developed in gsDesign Explorer version 2.0 on 12/16/2011 2:23:54 PM

###
# Design      : Design1
# Description : 
###

k <- 3
test.type <- 4
alpha <- 0.025
beta <- 0.1
n.fix <- 1
timing <- c(0.3333, 0.6667)
sfu <- sfLDOF
sfupar <- 0
sfl <- sfLDOF
sflpar <- 0
endpoint <- "user"

Design1 <- gsDesign(k=k, test.type=test.type, alpha=alpha, beta=beta, n.fix=n.fix,
  timing=timing, sfu=sfu, sfupar=sfupar, sfl=sfl, sflpar=sflpar, endpoint=endpoint)

