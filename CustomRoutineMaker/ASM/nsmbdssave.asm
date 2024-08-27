.nds
.create "out.bin", 0x00000000


.org	0x020ced54
bl	0x02000080


//ecode:
//.dw	 0xe2000080
//evalue:
//.dw	 0xe5c30000

.org	0x02000080



strb	r0,[r3]

strb	r0,[r3,-4]


bx	lr

.close
