##################################################################################
#  S3 methods for the gsDesign package
#
#  Exported Functions:
#                   
#    print.gsDesign
#    print.gsProbability
#    print.nSurvival
#    gsBoundSummary
#    xtable.gsDesign
#
#  Hidden Functions:
#
#    gsLegendText
#    gsPlotName
#    plotgsZ
#    plotBval
#    plotreleffect
#    plotgsCP
#    sfplot
#    plotASN
#    plotgsPower
#    sfprint
#
#  Author(s): Keaven Anderson, PhD.
# 
#  Reviewer(s): REvolution Computing 19DEC2008 v.2.0 - William Constantine, Kellie Wills 
#
#  R Version: 2.7.2
#
##################################################################################

###
# Exported Functions
###

"print.gsProbability" <- function(x, ...)
{    
    ntxt <- "N "
    nval <- ceiling(x$n.I)
    nspace <- log10(x$n.I[x$k])
    for (i in 1:nspace)
    {
        cat(" ")
    }
    
    cat("            ")
    if (min(x$lower$bound) < 0)
    {
        cat(" ")
    }
    if (max(class(x) == "gsBinomialExact")==1)
    {   cat("Bounds")
        y <- cbind(1:x$k, nval, x$lower$bound, round(x$upper$bound, 2))
        colnames(y) <- c("Analysis", "  N", "  a", "  b")
    }
    else
    {   cat("Lower bounds   Upper bounds")
        y <- cbind(1:x$k, nval, round(x$lower$bound, 2), round(pnorm(x$lower$bound), 4), 
                   round(x$upper$bound, 2), round(pnorm(-x$upper$bound), 4))
        colnames(y) <- c("Analysis", ntxt, "Z  ", "Nominal p", "Z  ", "Nominal p")
    }
    rownames(y) <- array(" ", x$k)
    cat("\n")
    print(y)
    cat("\nBoundary crossing probabilities and expected sample size assume\n")
    cat("any cross stops the trial\n\n")
    j <- length(x$theta)
    sump <- 1:j
    for (m in 1:j)
    {
        sump[m] <- sum(x$upper$prob[, m])
    }
    y <- round(cbind(x$theta, t(x$upper$prob), sump, x$en), 4)
    y[, x$k+3] <- round(y[, x$k+3], 1)
    rownames(y) <- array(" ", j)
    colnames(y) <- c("Theta", 1:x$k, "Total", "E{N}")
    cat("Upper boundary (power or Type I Error)\n")
    cat("          Analysis\n")
    print(y)
    
    for (m in 1:j) 
    {
        sump[m] <- sum(x$lower$prob[, m])
    }
    
    y <- round(cbind(x$theta, t(x$lower$prob), sump), 4)
    rownames(y) <- array(" ", j)
    colnames(y) <- c("Theta", 1:x$k, "Total")
    cat("\nLower boundary (futility or Type II Error)\n")
    cat("          Analysis\n")
    print(y)
}

"print.gsDesign" <- function(x, ...)
{    
    if (x$nFixSurv > 0)
    {    cat("Group sequential design sample size for time-to-event outcome\n", 
         "with sample size ", x$nSurv, ". The analysis plan below shows events\n",
         "at each analysis.\n", sep="")
    }
    
    if (x$test.type == 1) 
    {
        cat("One-sided")
    }
    else if (x$test.type == 2)
    {
        cat("Symmetric two-sided")
    }
    else 
    {
        cat("Asymmetric two-sided")
    }
    
    cat(" group sequential design with\n")
    cat(100 * (1 - x$beta), "% power and", 100 * x$alpha, "% Type I Error.\n")
    if (x$test.type > 1)
    {    
        if (x$test.type==4 || x$test.type==6)
        {
            cat("Upper bound spending computations assume\ntrial continues if lower bound is crossed.\n\n")            
        }
        else
        {
            cat("Spending computations assume trial stops\nif a bound is crossed.\n\n")
        }
    }
    if (x$n.fix != 1)
    {    
        ntxt <- "N "
        nval <- ceiling(x$n.I)
        nspace <- log10(x$n.I[x$k])
        for(i in 1:nspace)
        {
            cat(" ")
        }
        cat("            ")
    }
    else
    {    
        ntxt <- "Ratio*"
        nval <- round(x$n.I, 3)
        cat("           Sample\n")
        cat("            Size ")
    }
    if (x$test.type > 2) 
    {    
        if (min(x$lower$bound) < 0)
        {
            cat(" ")
        }
        cat("  ----Lower bounds----  ----Upper bounds-----")
        y <- cbind(1:x$k, nval, round(x$lower$bound, 2), round(pnorm(x$lower$bound), 4), 
                round(x$lower$spend, 4), round(x$upper$bound, 2), round(pnorm(-x$upper$bound), 4), 
                round(x$upper$spend, 4))
        colnames(y) <- c("Analysis", ntxt, "Z  ", "Nominal p", "Spend+", "Z  ", "Nominal p", "Spend++")
    }
    else
    {    y <- cbind(1:x$k, nval, round(x$upper$bound, 2), round(pnorm(-x$upper$bound), 4), 
                round(x$upper$spend, 4))
        colnames(y) <- c("Analysis", ntxt, "Z  ", "Nominal p", "Spend")
    }
    rownames(y) <- array(" ", x$k)
    cat("\n")
    print(y)
    cat("     Total")
    if (x$n.fix != 1)
    {    
        for (i in 1:nspace)
        {
            cat(" ")
        }
    }
    else 
    {
        cat("     ")
    }
    cat("                  ")
    
    if (x$test.type>2)
    {    
        if (min(x$lower$bound) < 0)
        {
            cat(" ")
        }
        cat(format(round(sum(x$lower$spend), 4), nsmall=4), "                ")
    }
    
    cat(format(round(sum(x$upper$spend), 4), nsmall=4), "\n")
    
    if (x$test.type > 4)
    {
        cat("+ lower bound spending (under H0):\n ")
    }
    else if (x$test.type > 2)
    {
        cat("+ lower bound beta spending (under H1):\n ")
    }
    
    if (x$test.type>2) 
    {
        sfprint(x$lower)
    }
    
    cat("++ alpha spending:\n ")
    sfprint(x$upper) 
    
    if (x$n.fix==1)
    {
        cat("* Sample size ratio compared to fixed design with no interim\n")
    }
    
    cat("\nBoundary crossing probabilities and expected sample size\nassume any cross stops the trial\n\n")
    j <- length(x$theta)
    sump <- 1:j
    
    for (m in 1:j)
    {
        sump[m] <- sum(x$upper$prob[, m])
    }
    
    y <- round(cbind(x$theta, t(x$upper$prob), sump, x$en), 4)
    if (x$n.fix != 1)
    {
        y[, x$k+3] <- round(y[, x$k+3], 1)
    }
    rownames(y) <- array(" ", j)
    colnames(y) <- c("Theta", 1:x$k, "Total", "E{N}")
    cat("Upper boundary (power or Type I Error)\n")
    cat("          Analysis\n")
    print(y)
    if (x$test.type>1)
    {    
        for (m in 1:j) 
        {
            sump[m] <- sum(x$lower$prob[, m])
        }
        y <- round(cbind(x$theta, t(x$lower$prob), sump), 4)
        rownames(y) <- array(" ", j)
        colnames(y) <- c("Theta", 1:x$k, "Total")
        cat("\nLower boundary (futility or Type II Error)\n")
        cat("          Analysis\n")
        print(y)
    }
}
print.nSurvival <- function(x,...){
	if (class(x) != "nSurvival") stop("print.nSurvival: primary argument must have class nSurvival")
   cat("Fixed design, two-arm trial with time-to-event\n")
	cat("outcome (Lachin and Foulkes, 1986).\n")
	cat("Study duration (fixed):          Ts=", x$Ts, "\n", sep="")
	cat("Accrual duration (fixed):        Tr=", x$Tr, "\n", sep="")
	if (x$entry=="unif") cat('Uniform accrual:              entry="unif"\n')
	else {
		cat('Exponential accrual:          entry="expo"\n') 
		cat("Accrual shape parameter:      gamma=", round(x$gamma,3), "\n",sep="")
	}
	cat("Control median:      log(2)/lambda1=", round(log(2) / x$lambda1,1), "\n", sep="")
	cat("Experimental median: log(2)/lambda2=", round(log(2) / x$lambda2,1), "\n", sep="")
	if (x$eta == 0){
		cat("Censoring only at study end (eta=0)\n")
	}else{
		cat("Censoring median:        log(2)/eta=", round(log(2) / x$eta, 1), "\n", sep="")
	}
	cat("Control failure rate:       lambda1=", round(x$lambda1,3), "\n", sep="") 
	cat("Experimental failure rate:  lambda2=", round(x$lambda2,3), "\n", sep="")
	cat("Censoring rate:                 eta=", round(x$eta,3),"\n", sep="")
	cat("Power:                 100*(1-beta)=", (1-x$beta)*100, "%\n",sep="")
   cat("Type I error (", x$sided, "-sided):   100*alpha=", 100*x$alpha, "%\n", sep="")
	if (x$ratio==1) cat("Equal randomization:          ratio=1\n")
	else cat("Randomization (Exp/Control):  ratio=", x$ratio, "\n", sep="")
	if (x$type=="rr"){
		cat("Sample size based on hazard ratio=", round(x$lambda2/x$lambda1,3), ' (type="rr")\n', sep="") 
  	}else{
		cat('Sample size based on risk difference=', round(x$lambda1 - x$lambda2,3), ' (type="rd")\n', sep="")
		if (x$approx) cat("Sample size based on H1 variance only:  approx=TRUE\n")
		else cat("Sample size based on H0 and H1 variance: approx=FALSE\n")
	}
   cat("Sample size (computed):           n=", 2*ceiling(x$n/2), "\n", sep="")
   cat("Events required (computed): nEvents=", ceiling(x$nEvents), "\n",sep="")
	invisible(x)
}

###
# Hidden Functions
###

"gsLegendText" <- function(test.type)
{
    switch(as.character(test.type), 
            "1" = c(expression(paste("Reject ", H[0])), "Continue"),
            "2" = c(expression(paste("Reject ", H[0])), "Continue", expression(paste("Reject ", H[0]))),
            c(expression(paste("Reject ", H[0])), "Continue", expression(paste("Reject ", H[1]))))            
}

"sfprint" <- function(x)
{   
    # print spending function information    
    if (x$name == "OF")
    {
        cat("O'Brien-Fleming boundary")
    }
    else if (x$name == "Pocock")
    {
        cat("Pocock boundary")
    }
    else if (x$name == "WT")
    {
        cat("Wang-Tsiatis boundary with Delta =", x$param)
    }
    else if (x$name == "Truncated")
    {   cat(x$param$name,"spending function truncated at",x$param$trange)
        if (!is.null(x$parname) && !is.null(x$param))
        {
            cat("\n with", x$parname, "=", x$param$param)
        }
    }
    else 
    {   
        cat(x$name, "spending function")
        if (!is.null(x$parname) && !is.null(x$param))
        {
            cat(" with", x$parname, "=", x$param)
        }
    }
    cat("\n")
}

gsBoundSummary <- function(x, upper=TRUE, ratio=1)
{   if (upper)
    {   sn <- -1
        bnd <- x$upper
    }else
    {   sn <- 1
        bnd <- x$lower
    }
    p0 <- cumsum(bnd$prob[,1])
    p1 <- cumsum(bnd$prob[,x$theta==x$delta])
    CP <- p0
    CP[x$k] <- NA
    CPhat <- CP
    for(i in 1:x$k)
    {   if (i < x$k)
        {   CPtem <- gsCP(zi=bnd$bound[i], i=i, x=x, theta=NULL)$upper$prob
            CP[i] <- sum(CPtem[,3])
            CPhat[i] <- sum(CPtem[,1])
        }
    }
    if (is.null(x$endpoint) || tolower(x$endpoint)!="survival") 
    {    effect <- gsDelta(bnd$bound, i=1:x$k, x=x)
         ename <- "delta"
    }else
    {    effect <- gsHR(z=bnd$bound, i=1:x$k, x=x, ratio=1)
         ename <- "HR"
    }
    nval <- x$n.I
    if (x$n.fix != 1) nval <- ceiling(nval/2)*2 
    tab <- cbind(100*x$timing, nval, bnd$bound, pnorm(sn * bnd$bound), p0, p1, CP, CPhat,
              effect,
                 gsBValue(bnd$bound, i=1:x$k, x=x))
    rnames <- c(paste("IA",1:(x$k-1)), "Final")
    cnames <- c("Timing (%)", ifelse(x$n.fix==1,"r","N"), "Z", "Nominal p", "H0 spend", "H1 spend",
                "CP theta1", "CP thetahat", ename , "B-value")
    dimnames(tab) <- list(rnames, cnames)
    gss <- list(upper=upper, tab=t(tab))
    class(gss) <- "gsBoundSummary"
    gss
}
xtable.gsDesign <- function(x, caption=NULL, label=NULL, align=NULL, digits=c(0,0,3,4,4,4,3,3,3,3),
                             display=NULL, upper=TRUE, rnames=NULL, cnames=NULL, ratio=1,
                             sanitize.text.function=function(x){x}, 
                             sanitize.rownames.function=function(x){x},...)
{  bnd <- round(gsBoundSummary(x, upper, ratio)$tab, digits)
   if (is.null(cnames)) cnames <- colnames(bnd)
   if (is.null(rnames))
   {   rnames <- rownames(bnd)
       if (rnames[9] == "delta") ename <- "$\\hat{\\delta}$"
       else if (rnames[9] == "HR") ename <- "HR"

       if (x$n.fix==1) nname <- "r"
       else if (!is.null(x$endpoint) && tolower(x$endpoint)=="survival") nname <- "Events"  
       else nname <- "N"
       rnames <- c("Timing (\\%)", nname, "Z", "Nominal p", "H$_0$-spend", "$\\beta$-spend",
                   "$\\hbox{CP}(\\theta_1,\\hbox{Z})$", "$\\hbox{CP}(\\hat{\\theta}, \\hbox{Z})$",
                   ename, "B-value")
   }
   b <- matrix(as.character(bnd), nrow=nrow(bnd), ncol=ncol(bnd),
               dimnames=list(rnames,cnames))
   b
   print(xtable(b, caption=caption, label=label, display=display, align=align,
                   sanitize.text.function=sanitize.text.function,...),
                   sanitize.rownames.function=sanitize.rownames.function)
}
print.gsBoundSummary <- function(x, digits=c(0,0,3,4,4,4,3,3,3,3),cnames=NULL,
   rnames=c("Timing (%)","N","Z","Nominal p","H0 spend","H1 spend",
"CP theta1","CP thetahat","delta","B-value"),quote=F){
	bnd <- round(x$tab,digits)
	if (is.null(cnames)) cnames <- colnames(bnd)
	b <- matrix(as.character(bnd), nrow=nrow(bnd), ncol=ncol(bnd),
               dimnames=list(rnames,cnames))
	print(ifelse(x$upper,"Upper bound","Lower bound"))
	print(b,quote=quote)
}

