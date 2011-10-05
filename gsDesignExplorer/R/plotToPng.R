plotToPng = function(plotfunc) {
	require(Cairo)
	require(ggplot2)
	require(png)

	Cairo(file='/dev/null')

	plotfunc()

	i = Cairo:::.image(dev.cur())
	r = Cairo:::.ptr.to.raw(i$ref, 0, i$width * i$height * 4)
	dim(r) = c(4, i$width, i$height) # RGBA planes
	r[c(1,3),,] = r[c(3,1),,]
	p = writePNG(r, raw()) # raw PNG bytes

	# close device (restore previous)
	dev.off()

	return (p)
}
