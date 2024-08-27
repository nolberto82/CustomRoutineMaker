.psp
.create "out.bin", 0x08800000


.org	0x0990f3e0
j	0x08801060


.org	0x08801060


li	a0,4
sw	a0,(s1)



lwc1	f28,0xf4(sp)
j	0x0990f3e8
.close
