.psx
.create "out.bin", 0x80000000


.org	0x80010000
j	0x80007600


.org	0x80007600






j	0x80010008
.close
