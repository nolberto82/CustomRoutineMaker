.n64
.create "out.bin", 0x80000000


.org	0x80000000
j	0x80400000


.org	0x80400000



lui	v0,0x1234


j	0x80400008
.close
