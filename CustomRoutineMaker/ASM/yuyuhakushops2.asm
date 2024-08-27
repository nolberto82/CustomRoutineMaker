.ps2
.create "out.bin", 0x20000000


.org	 0x20241214
j	 0x200a0000
add	 r0,r0

//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000

.org	 0x200a0000


lw 	a0,0x24(s1)
li 	t0,0x305f7469
lw	t1,0x10(sp)
lw	t1,0x14(t1)
bne	t0,t1,end
add	r0,r0
j	0x20241254
end:
add	r0,r0
j 	0x20241214+8

//.org	0x20241238
//.dw	0x10400063
.close
