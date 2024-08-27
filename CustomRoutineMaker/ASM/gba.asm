.gba
.thumb
.create "out.bin", 0x00000000


.org	0x0203ff00
ldr	r0,=0x0203ff00
bx	r0
.pool


.org	0x0203ff00



mov	r7,0x63


bx	r0
.pool
.close
