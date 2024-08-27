.psx

.create "out.bin", 0x80000000



.org	0x80020218


j	0x800083d0




.org	0x800083d0


lw	v0,0xe0(s2)


la	at,0x05e1fe1f

bne	at,s1,end

lw	s5,(v0)


la	at,0x8257c010

bne	s5,at,end
nop


la	at,0x06e1fe00
sw	at,4(v0)

end:
j	0x80020220
.close
