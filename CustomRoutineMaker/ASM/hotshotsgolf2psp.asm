.psp
.create "out.bin", 0x00000000
.definelabel hook, 0x08845174
.definelabel function, 0x08801000

.org	hook
j	function

//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	0x00000000

.org	function


lhu	t0,0x08b83544
andi	t0,t0,0x40
beq	t0,zero,end
nop
sw	v0,0xbd0(s5)
j	hook+40

end:
lw	v1,0xbd0(s5)
j	hook+8
.close
