//.swiswi64

.text

.org	0x0031f178-0x4000
bl	jumpspeed

.org 	0x00415990-0x4000
ret

.org	0x00415994-0x4000
jumpspeed:

stp 	s1, s2, [x8,#0x24]

ldr	x0,[x8,-8]
ldr     x0,[x0,0x1d8]
ldrb	w0,[x0,0x54]
tbz	w0,0,end

mov	w0,0x41c0
str	w0,[x8,#0x2a]

end:
ret






