//
// KeyInfoX509DataTest.cs - NUnit Test Cases for KeyInfoX509Data
//
// Author:
//	Sebastien Pouliot (spouliot@motus.com)
//
// (C) 2002, 2003 Motus Technologies Inc. (http://www.motus.com)
//

using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

using NUnit.Framework;

namespace MonoTests.System.Security.Cryptography.Xml {

	[TestFixture]
	public class KeyInfoX509DataTest {

		static byte[] cert = { 0x30,0x82,0x09,0xB9,0x30,0x82,0x09,0x22,0xA0,0x03,0x02,0x01,0x02,0x02,0x10,0x20,0x0B,0x35,0x5E,0xCE,0xC4,0xB0,0x63,0xB7,0xDE,0xC6,0x34,0xB9,0x70,0x34,0x44,0x30,0x0D,0x06,0x09,0x2A,0x86,0x48,0x86,0xF7,0x0D,0x01,0x01,0x04,0x05,0x00,0x30,0x62,0x31,0x11,0x30,0x0F,0x06,0x03,0x55,0x04,0x07,0x13,0x08,0x49,0x6E,0x74,0x65,0x72,0x6E,0x65,0x74,0x31,0x17,0x30,0x15,0x06,0x03,0x55,0x04,0x0A,0x13,0x0E,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x2C,0x20,0x49,0x6E,0x63,0x2E,0x31,0x34,0x30,0x32,0x06,0x03,0x55,0x04,0x0B,
			0x13,0x2B,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x20,0x43,0x6C,0x61,0x73,0x73,0x20,0x31,0x20,0x43,0x41,0x20,0x2D,0x20,0x49,0x6E,0x64,0x69,0x76,0x69,0x64,0x75,0x61,0x6C,0x20,0x53,0x75,0x62,0x73,0x63,0x72,0x69,0x62,0x65,0x72,0x30,0x1E,0x17,0x0D,0x39,0x36,0x30,0x38,0x32,0x31,0x30,0x30,0x30,0x30,0x30,0x30,0x5A,0x17,0x0D,0x39,0x37,0x30,0x38,0x32,0x30,0x32,0x33,0x35,0x39,0x35,0x39,0x5A,0x30,0x82,0x01,0x0A,0x31,0x11,0x30,0x0F,0x06,0x03,0x55,0x04,0x07,0x13,0x08,0x49,0x6E,0x74,0x65,0x72,0x6E,0x65,0x74,
			0x31,0x17,0x30,0x15,0x06,0x03,0x55,0x04,0x0A,0x13,0x0E,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x2C,0x20,0x49,0x6E,0x63,0x2E,0x31,0x34,0x30,0x32,0x06,0x03,0x55,0x04,0x0B,0x13,0x2B,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x20,0x43,0x6C,0x61,0x73,0x73,0x20,0x31,0x20,0x43,0x41,0x20,0x2D,0x20,0x49,0x6E,0x64,0x69,0x76,0x69,0x64,0x75,0x61,0x6C,0x20,0x53,0x75,0x62,0x73,0x63,0x72,0x69,0x62,0x65,0x72,0x31,0x46,0x30,0x44,0x06,0x03,0x55,0x04,0x0B,0x13,0x3D,0x77,0x77,0x77,0x2E,0x76,0x65,0x72,0x69,0x73,0x69,
			0x67,0x6E,0x2E,0x63,0x6F,0x6D,0x2F,0x72,0x65,0x70,0x6F,0x73,0x69,0x74,0x6F,0x72,0x79,0x2F,0x43,0x50,0x53,0x20,0x49,0x6E,0x63,0x6F,0x72,0x70,0x2E,0x20,0x62,0x79,0x20,0x52,0x65,0x66,0x2E,0x2C,0x4C,0x49,0x41,0x42,0x2E,0x4C,0x54,0x44,0x28,0x63,0x29,0x39,0x36,0x31,0x26,0x30,0x24,0x06,0x03,0x55,0x04,0x0B,0x13,0x1D,0x44,0x69,0x67,0x69,0x74,0x61,0x6C,0x20,0x49,0x44,0x20,0x43,0x6C,0x61,0x73,0x73,0x20,0x31,0x20,0x2D,0x20,0x4E,0x65,0x74,0x73,0x63,0x61,0x70,0x65,0x31,0x16,0x30,0x14,0x06,0x03,0x55,0x04,0x03,
			0x13,0x0D,0x44,0x61,0x76,0x69,0x64,0x20,0x54,0x2E,0x20,0x47,0x72,0x61,0x79,0x31,0x1E,0x30,0x1C,0x06,0x09,0x2A,0x86,0x48,0x86,0xF7,0x0D,0x01,0x09,0x01,0x16,0x0F,0x64,0x61,0x76,0x69,0x64,0x40,0x66,0x6F,0x72,0x6D,0x61,0x6C,0x2E,0x69,0x65,0x30,0x5C,0x30,0x0D,0x06,0x09,0x2A,0x86,0x48,0x86,0xF7,0x0D,0x01,0x01,0x01,0x05,0x00,0x03,0x4B,0x00,0x30,0x48,0x02,0x41,0x00,0xC5,0x81,0x07,0xA2,0xEB,0x0F,0xB8,0xFF,0xF8,0xF8,0x1C,0xEE,0x32,0xFF,0xBF,0x12,0x35,0x6A,0xF9,0x6B,0xC8,0xBE,0x2F,0xFB,0x3E,0xAF,0x04,0x51,
			0x4A,0xAC,0xDD,0x10,0x29,0xA8,0xCD,0x40,0x5B,0x66,0x1E,0x98,0xEF,0xF2,0x4C,0x77,0xFA,0x8F,0x86,0xD1,0x21,0x67,0x92,0x44,0x4A,0xC4,0x89,0xC9,0x83,0xCF,0x88,0x9F,0x6F,0xE2,0x32,0x35,0x02,0x03,0x01,0x00,0x01,0xA3,0x82,0x07,0x08,0x30,0x82,0x07,0x04,0x30,0x09,0x06,0x03,0x55,0x1D,0x13,0x04,0x02,0x30,0x00,0x30,0x82,0x02,0x1F,0x06,0x03,0x55,0x1D,0x03,0x04,0x82,0x02,0x16,0x30,0x82,0x02,0x12,0x30,0x82,0x02,0x0E,0x30,0x82,0x02,0x0A,0x06,0x0B,0x60,0x86,0x48,0x01,0x86,0xF8,0x45,0x01,0x07,0x01,0x01,0x30,0x82,
			0x01,0xF9,0x16,0x82,0x01,0xA7,0x54,0x68,0x69,0x73,0x20,0x63,0x65,0x72,0x74,0x69,0x66,0x69,0x63,0x61,0x74,0x65,0x20,0x69,0x6E,0x63,0x6F,0x72,0x70,0x6F,0x72,0x61,0x74,0x65,0x73,0x20,0x62,0x79,0x20,0x72,0x65,0x66,0x65,0x72,0x65,0x6E,0x63,0x65,0x2C,0x20,0x61,0x6E,0x64,0x20,0x69,0x74,0x73,0x20,0x75,0x73,0x65,0x20,0x69,0x73,0x20,0x73,0x74,0x72,0x69,0x63,0x74,0x6C,0x79,0x20,0x73,0x75,0x62,0x6A,0x65,0x63,0x74,0x20,0x74,0x6F,0x2C,0x20,0x74,0x68,0x65,0x20,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x20,0x43,
			0x65,0x72,0x74,0x69,0x66,0x69,0x63,0x61,0x74,0x69,0x6F,0x6E,0x20,0x50,0x72,0x61,0x63,0x74,0x69,0x63,0x65,0x20,0x53,0x74,0x61,0x74,0x65,0x6D,0x65,0x6E,0x74,0x20,0x28,0x43,0x50,0x53,0x29,0x2C,0x20,0x61,0x76,0x61,0x69,0x6C,0x61,0x62,0x6C,0x65,0x20,0x61,0x74,0x3A,0x20,0x68,0x74,0x74,0x70,0x73,0x3A,0x2F,0x2F,0x77,0x77,0x77,0x2E,0x76,0x65,0x72,0x69,0x73,0x69,0x67,0x6E,0x2E,0x63,0x6F,0x6D,0x2F,0x43,0x50,0x53,0x3B,0x20,0x62,0x79,0x20,0x45,0x2D,0x6D,0x61,0x69,0x6C,0x20,0x61,0x74,0x20,0x43,0x50,0x53,0x2D,
			0x72,0x65,0x71,0x75,0x65,0x73,0x74,0x73,0x40,0x76,0x65,0x72,0x69,0x73,0x69,0x67,0x6E,0x2E,0x63,0x6F,0x6D,0x3B,0x20,0x6F,0x72,0x20,0x62,0x79,0x20,0x6D,0x61,0x69,0x6C,0x20,0x61,0x74,0x20,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x2C,0x20,0x49,0x6E,0x63,0x2E,0x2C,0x20,0x32,0x35,0x39,0x33,0x20,0x43,0x6F,0x61,0x73,0x74,0x20,0x41,0x76,0x65,0x2E,0x2C,0x20,0x4D,0x6F,0x75,0x6E,0x74,0x61,0x69,0x6E,0x20,0x56,0x69,0x65,0x77,0x2C,0x20,0x43,0x41,0x20,0x39,0x34,0x30,0x34,0x33,0x20,0x55,0x53,0x41,0x20,0x54,0x65,
			0x6C,0x2E,0x20,0x2B,0x31,0x20,0x28,0x34,0x31,0x35,0x29,0x20,0x39,0x36,0x31,0x2D,0x38,0x38,0x33,0x30,0x20,0x43,0x6F,0x70,0x79,0x72,0x69,0x67,0x68,0x74,0x20,0x28,0x63,0x29,0x20,0x31,0x39,0x39,0x36,0x20,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x2C,0x20,0x49,0x6E,0x63,0x2E,0x20,0x20,0x41,0x6C,0x6C,0x20,0x52,0x69,0x67,0x68,0x74,0x73,0x20,0x52,0x65,0x73,0x65,0x72,0x76,0x65,0x64,0x2E,0x20,0x43,0x45,0x52,0x54,0x41,0x49,0x4E,0x20,0x57,0x41,0x52,0x52,0x41,0x4E,0x54,0x49,0x45,0x53,0x20,0x44,0x49,0x53,0x43,
			0x4C,0x41,0x49,0x4D,0x45,0x44,0x20,0x61,0x6E,0x64,0x20,0x4C,0x49,0x41,0x42,0x49,0x4C,0x49,0x54,0x59,0x20,0x4C,0x49,0x4D,0x49,0x54,0x45,0x44,0x2E,0xA0,0x0E,0x06,0x0C,0x60,0x86,0x48,0x01,0x86,0xF8,0x45,0x01,0x07,0x01,0x01,0x01,0xA1,0x0E,0x06,0x0C,0x60,0x86,0x48,0x01,0x86,0xF8,0x45,0x01,0x07,0x01,0x01,0x02,0x30,0x2C,0x30,0x2A,0x16,0x28,0x68,0x74,0x74,0x70,0x73,0x3A,0x2F,0x2F,0x77,0x77,0x77,0x2E,0x76,0x65,0x72,0x69,0x73,0x69,0x67,0x6E,0x2E,0x63,0x6F,0x6D,0x2F,0x72,0x65,0x70,0x6F,0x73,0x69,0x74,0x6F,
			0x72,0x79,0x2F,0x43,0x50,0x53,0x20,0x30,0x11,0x06,0x09,0x60,0x86,0x48,0x01,0x86,0xF8,0x42,0x01,0x01,0x04,0x04,0x03,0x02,0x07,0x80,0x30,0x36,0x06,0x09,0x60,0x86,0x48,0x01,0x86,0xF8,0x42,0x01,0x08,0x04,0x29,0x16,0x27,0x68,0x74,0x74,0x70,0x73,0x3A,0x2F,0x2F,0x77,0x77,0x77,0x2E,0x76,0x65,0x72,0x69,0x73,0x69,0x67,0x6E,0x2E,0x63,0x6F,0x6D,0x2F,0x72,0x65,0x70,0x6F,0x73,0x69,0x74,0x6F,0x72,0x79,0x2F,0x43,0x50,0x53,0x30,0x82,0x04,0x87,0x06,0x09,0x60,0x86,0x48,0x01,0x86,0xF8,0x42,0x01,0x0D,0x04,0x82,0x04,
			0x78,0x16,0x82,0x04,0x74,0x43,0x41,0x55,0x54,0x49,0x4F,0x4E,0x3A,0x20,0x54,0x68,0x65,0x20,0x43,0x6F,0x6D,0x6D,0x6F,0x6E,0x20,0x4E,0x61,0x6D,0x65,0x20,0x69,0x6E,0x20,0x74,0x68,0x69,0x73,0x20,0x43,0x6C,0x61,0x73,0x73,0x20,0x31,0x20,0x44,0x69,0x67,0x69,0x74,0x61,0x6C,0x20,0x0A,0x49,0x44,0x20,0x69,0x73,0x20,0x6E,0x6F,0x74,0x20,0x61,0x75,0x74,0x68,0x65,0x6E,0x74,0x69,0x63,0x61,0x74,0x65,0x64,0x20,0x62,0x79,0x20,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x2E,0x20,0x49,0x74,0x20,0x6D,0x61,0x79,0x20,0x62,
			0x65,0x20,0x74,0x68,0x65,0x0A,0x68,0x6F,0x6C,0x64,0x65,0x72,0x27,0x73,0x20,0x72,0x65,0x61,0x6C,0x20,0x6E,0x61,0x6D,0x65,0x20,0x6F,0x72,0x20,0x61,0x6E,0x20,0x61,0x6C,0x69,0x61,0x73,0x2E,0x20,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x20,0x64,0x6F,0x65,0x73,0x20,0x61,0x75,0x74,0x68,0x2D,0x0A,0x65,0x6E,0x74,0x69,0x63,0x61,0x74,0x65,0x20,0x74,0x68,0x65,0x20,0x65,0x2D,0x6D,0x61,0x69,0x6C,0x20,0x61,0x64,0x64,0x72,0x65,0x73,0x73,0x20,0x6F,0x66,0x20,0x74,0x68,0x65,0x20,0x68,0x6F,0x6C,0x64,0x65,0x72,0x2E,
			0x0A,0x0A,0x54,0x68,0x69,0x73,0x20,0x63,0x65,0x72,0x74,0x69,0x66,0x69,0x63,0x61,0x74,0x65,0x20,0x69,0x6E,0x63,0x6F,0x72,0x70,0x6F,0x72,0x61,0x74,0x65,0x73,0x20,0x62,0x79,0x20,0x72,0x65,0x66,0x65,0x72,0x65,0x6E,0x63,0x65,0x2C,0x20,0x61,0x6E,0x64,0x20,0x0A,0x69,0x74,0x73,0x20,0x75,0x73,0x65,0x20,0x69,0x73,0x20,0x73,0x74,0x72,0x69,0x63,0x74,0x6C,0x79,0x20,0x73,0x75,0x62,0x6A,0x65,0x63,0x74,0x20,0x74,0x6F,0x2C,0x20,0x74,0x68,0x65,0x20,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x20,0x0A,0x43,0x65,0x72,
			0x74,0x69,0x66,0x69,0x63,0x61,0x74,0x69,0x6F,0x6E,0x20,0x50,0x72,0x61,0x63,0x74,0x69,0x63,0x65,0x20,0x53,0x74,0x61,0x74,0x65,0x6D,0x65,0x6E,0x74,0x20,0x28,0x43,0x50,0x53,0x29,0x2C,0x20,0x61,0x76,0x61,0x69,0x6C,0x61,0x62,0x6C,0x65,0x0A,0x69,0x6E,0x20,0x74,0x68,0x65,0x20,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x20,0x72,0x65,0x70,0x6F,0x73,0x69,0x74,0x6F,0x72,0x79,0x20,0x61,0x74,0x3A,0x20,0x0A,0x68,0x74,0x74,0x70,0x73,0x3A,0x2F,0x2F,0x77,0x77,0x77,0x2E,0x76,0x65,0x72,0x69,0x73,0x69,0x67,0x6E,0x2E,
			0x63,0x6F,0x6D,0x3B,0x20,0x62,0x79,0x20,0x45,0x2D,0x6D,0x61,0x69,0x6C,0x20,0x61,0x74,0x0A,0x43,0x50,0x53,0x2D,0x72,0x65,0x71,0x75,0x65,0x73,0x74,0x73,0x40,0x76,0x65,0x72,0x69,0x73,0x69,0x67,0x6E,0x2E,0x63,0x6F,0x6D,0x3B,0x20,0x6F,0x72,0x20,0x62,0x79,0x20,0x6D,0x61,0x69,0x6C,0x20,0x61,0x74,0x20,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x2C,0x0A,0x49,0x6E,0x63,0x2E,0x2C,0x20,0x32,0x35,0x39,0x33,0x20,0x43,0x6F,0x61,0x73,0x74,0x20,0x41,0x76,0x65,0x2E,0x2C,0x20,0x4D,0x6F,0x75,0x6E,0x74,0x61,0x69,0x6E,
			0x20,0x56,0x69,0x65,0x77,0x2C,0x20,0x43,0x41,0x20,0x39,0x34,0x30,0x34,0x33,0x20,0x55,0x53,0x41,0x0A,0x0A,0x43,0x6F,0x70,0x79,0x72,0x69,0x67,0x68,0x74,0x20,0x28,0x63,0x29,0x31,0x39,0x39,0x36,0x20,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x2C,0x20,0x49,0x6E,0x63,0x2E,0x20,0x20,0x41,0x6C,0x6C,0x20,0x52,0x69,0x67,0x68,0x74,0x73,0x20,0x0A,0x52,0x65,0x73,0x65,0x72,0x76,0x65,0x64,0x2E,0x20,0x43,0x45,0x52,0x54,0x41,0x49,0x4E,0x20,0x57,0x41,0x52,0x52,0x41,0x4E,0x54,0x49,0x45,0x53,0x20,0x44,0x49,0x53,0x43,
			0x4C,0x41,0x49,0x4D,0x45,0x44,0x20,0x41,0x4E,0x44,0x20,0x0A,0x4C,0x49,0x41,0x42,0x49,0x4C,0x49,0x54,0x59,0x20,0x4C,0x49,0x4D,0x49,0x54,0x45,0x44,0x2E,0x0A,0x0A,0x57,0x41,0x52,0x4E,0x49,0x4E,0x47,0x3A,0x20,0x54,0x48,0x45,0x20,0x55,0x53,0x45,0x20,0x4F,0x46,0x20,0x54,0x48,0x49,0x53,0x20,0x43,0x45,0x52,0x54,0x49,0x46,0x49,0x43,0x41,0x54,0x45,0x20,0x49,0x53,0x20,0x53,0x54,0x52,0x49,0x43,0x54,0x4C,0x59,0x0A,0x53,0x55,0x42,0x4A,0x45,0x43,0x54,0x20,0x54,0x4F,0x20,0x54,0x48,0x45,0x20,0x56,0x45,0x52,0x49,
			0x53,0x49,0x47,0x4E,0x20,0x43,0x45,0x52,0x54,0x49,0x46,0x49,0x43,0x41,0x54,0x49,0x4F,0x4E,0x20,0x50,0x52,0x41,0x43,0x54,0x49,0x43,0x45,0x0A,0x53,0x54,0x41,0x54,0x45,0x4D,0x45,0x4E,0x54,0x2E,0x20,0x20,0x54,0x48,0x45,0x20,0x49,0x53,0x53,0x55,0x49,0x4E,0x47,0x20,0x41,0x55,0x54,0x48,0x4F,0x52,0x49,0x54,0x59,0x20,0x44,0x49,0x53,0x43,0x4C,0x41,0x49,0x4D,0x53,0x20,0x43,0x45,0x52,0x54,0x41,0x49,0x4E,0x0A,0x49,0x4D,0x50,0x4C,0x49,0x45,0x44,0x20,0x41,0x4E,0x44,0x20,0x45,0x58,0x50,0x52,0x45,0x53,0x53,0x20,
			0x57,0x41,0x52,0x52,0x41,0x4E,0x54,0x49,0x45,0x53,0x2C,0x20,0x49,0x4E,0x43,0x4C,0x55,0x44,0x49,0x4E,0x47,0x20,0x57,0x41,0x52,0x52,0x41,0x4E,0x54,0x49,0x45,0x53,0x0A,0x4F,0x46,0x20,0x4D,0x45,0x52,0x43,0x48,0x41,0x4E,0x54,0x41,0x42,0x49,0x4C,0x49,0x54,0x59,0x20,0x4F,0x52,0x20,0x46,0x49,0x54,0x4E,0x45,0x53,0x53,0x20,0x46,0x4F,0x52,0x20,0x41,0x20,0x50,0x41,0x52,0x54,0x49,0x43,0x55,0x4C,0x41,0x52,0x0A,0x50,0x55,0x52,0x50,0x4F,0x53,0x45,0x2C,0x20,0x41,0x4E,0x44,0x20,0x57,0x49,0x4C,0x4C,0x20,0x4E,0x4F,
			0x54,0x20,0x42,0x45,0x20,0x4C,0x49,0x41,0x42,0x4C,0x45,0x20,0x46,0x4F,0x52,0x20,0x43,0x4F,0x4E,0x53,0x45,0x51,0x55,0x45,0x4E,0x54,0x49,0x41,0x4C,0x2C,0x0A,0x50,0x55,0x4E,0x49,0x54,0x49,0x56,0x45,0x2C,0x20,0x41,0x4E,0x44,0x20,0x43,0x45,0x52,0x54,0x41,0x49,0x4E,0x20,0x4F,0x54,0x48,0x45,0x52,0x20,0x44,0x41,0x4D,0x41,0x47,0x45,0x53,0x2E,0x20,0x53,0x45,0x45,0x20,0x54,0x48,0x45,0x20,0x43,0x50,0x53,0x0A,0x46,0x4F,0x52,0x20,0x44,0x45,0x54,0x41,0x49,0x4C,0x53,0x2E,0x0A,0x0A,0x43,0x6F,0x6E,0x74,0x65,0x6E,
			0x74,0x73,0x20,0x6F,0x66,0x20,0x74,0x68,0x65,0x20,0x56,0x65,0x72,0x69,0x53,0x69,0x67,0x6E,0x20,0x72,0x65,0x67,0x69,0x73,0x74,0x65,0x72,0x65,0x64,0x0A,0x6E,0x6F,0x6E,0x76,0x65,0x72,0x69,0x66,0x69,0x65,0x64,0x53,0x75,0x62,0x6A,0x65,0x63,0x74,0x41,0x74,0x74,0x72,0x69,0x62,0x75,0x74,0x65,0x73,0x20,0x65,0x78,0x74,0x65,0x6E,0x73,0x69,0x6F,0x6E,0x20,0x76,0x61,0x6C,0x75,0x65,0x20,0x73,0x68,0x61,0x6C,0x6C,0x20,0x0A,0x6E,0x6F,0x74,0x20,0x62,0x65,0x20,0x63,0x6F,0x6E,0x73,0x69,0x64,0x65,0x72,0x65,0x64,0x20,
			0x61,0x73,0x20,0x61,0x63,0x63,0x75,0x72,0x61,0x74,0x65,0x20,0x69,0x6E,0x66,0x6F,0x72,0x6D,0x61,0x74,0x69,0x6F,0x6E,0x20,0x76,0x61,0x6C,0x69,0x64,0x61,0x74,0x65,0x64,0x20,0x0A,0x62,0x79,0x20,0x74,0x68,0x65,0x20,0x49,0x41,0x2E,0x30,0x0D,0x06,0x09,0x2A,0x86,0x48,0x86,0xF7,0x0D,0x01,0x01,0x04,0x05,0x00,0x03,0x81,0x81,0x00,0x2B,0x3D,0x44,0xC7,0x32,0x59,0xAE,0xF1,0x5F,0x8F,0x3F,0x87,0xE3,0x3E,0xEB,0x81,0x30,0xF8,0xA9,0x96,0xDB,0x01,0x42,0x0B,0x04,0xEF,0x37,0x02,0x3F,0xD4,0x20,0x61,0x58,0xC4,0x4A,0x3A,
			0x39,0xB3,0xFB,0xD9,0xF8,0xA5,0xC4,0x5E,0x33,0x5A,0x0E,0xFA,0x93,0x56,0x2F,0x6F,0xD6,0x61,0xA2,0xAF,0xA5,0x0C,0x1D,0xE2,0x41,0x65,0xF3,0x40,0x75,0x66,0x83,0xD2,0x5A,0xB4,0xB7,0x56,0x0B,0x8E,0x0D,0xA1,0x33,0x13,0x7D,0x49,0xC3,0xB1,0x00,0x68,0x83,0x7F,0xB5,0x66,0xD4,0x32,0x32,0xFE,0x8B,0x9A,0x5A,0xD6,0x01,0x72,0x31,0x5D,0x85,0x91,0xBC,0x93,0x9B,0x65,0x60,0x25,0xC6,0x1F,0xBC,0xDD,0x69,0x44,0x62,0xC2,0xB2,0x6F,0x46,0xAB,0x2F,0x20,0xA5,0x6F,0xDA,0x48,0x6C,0x9C };

		static byte[] cert2 = { 0x30,0x82,0x02,0x1D,0x30,0x82,0x01,0x86,0x02,0x01,0x14,0x30,0x0D,0x06,0x09,0x2A,0x86,0x48,0x86,0xF7,0x0D,0x01,0x01,0x04,0x05,0x00,0x30,0x58,0x31,0x0B,0x30,0x09,0x06,0x03,0x55,0x04,0x06,0x13,0x02,0x43,0x41,0x31,0x1F,0x30,0x1D,0x06,0x03,0x55,0x04,0x03,0x13,0x16,0x4B,0x65,0x79,0x77,0x69,0x74,0x6E,0x65,0x73,0x73,0x20,0x43,0x61,0x6E,0x61,0x64,0x61,0x20,0x49,0x6E,0x63,0x2E,0x31,0x28,0x30,0x26,0x06,0x0A,0x2B,0x06,0x01,0x04,0x01,0x2A,0x02,0x0B,0x02,0x01,0x13,0x18,0x6B,0x65,0x79,0x77,0x69,0x74,0x6E,0x65,0x73,
			0x73,0x40,0x6B,0x65,0x79,0x77,0x69,0x74,0x6E,0x65,0x73,0x73,0x2E,0x63,0x61,0x30,0x1E,0x17,0x0D,0x39,0x36,0x30,0x35,0x30,0x37,0x30,0x30,0x30,0x30,0x30,0x30,0x5A,0x17,0x0D,0x39,0x39,0x30,0x35,0x30,0x37,0x30,0x30,0x30,0x30,0x30,0x30,0x5A,0x30,0x58,0x31,0x0B,0x30,0x09,0x06,0x03,0x55,0x04,0x06,0x13,0x02,0x43,0x41,0x31,0x1F,0x30,0x1D,0x06,0x03,0x55,0x04,0x03,0x13,0x16,0x4B,0x65,0x79,0x77,0x69,0x74,0x6E,0x65,0x73,0x73,0x20,0x43,0x61,0x6E,0x61,0x64,0x61,0x20,0x49,0x6E,0x63,0x2E,0x31,0x28,0x30,0x26,0x06,
			0x0A,0x2B,0x06,0x01,0x04,0x01,0x2A,0x02,0x0B,0x02,0x01,0x13,0x18,0x6B,0x65,0x79,0x77,0x69,0x74,0x6E,0x65,0x73,0x73,0x40,0x6B,0x65,0x79,0x77,0x69,0x74,0x6E,0x65,0x73,0x73,0x2E,0x63,0x61,0x30,0x81,0x9D,0x30,0x0D,0x06,0x09,0x2A,0x86,0x48,0x86,0xF7,0x0D,0x01,0x01,0x01,0x05,0x00,0x03,0x81,0x8B,0x00,0x30,0x81,0x87,0x02,0x81,0x81,0x00,0xCD,0x23,0xFA,0x2A,0xE1,0xED,0x98,0xF4,0xE9,0xD0,0x93,0x3E,0xD7,0x7A,0x80,0x02,0x4C,0xCC,0xC1,0x02,0xAF,0x5C,0xB6,0x1F,0x7F,0xFA,0x57,0x42,0x6F,0x30,0xD1,0x20,0xC5,0xB5,
			0x21,0x07,0x40,0x2C,0xA9,0x86,0xC2,0xF3,0x64,0x84,0xAE,0x3D,0x85,0x2E,0xED,0x85,0xBD,0x54,0xB0,0x18,0x28,0xEF,0x6A,0xF8,0x1B,0xE7,0x0B,0x16,0x1F,0x93,0x25,0x4F,0xC7,0xF8,0x8E,0xC3,0xB9,0xCA,0x98,0x84,0x0E,0x55,0xD0,0x2F,0xEF,0x78,0x77,0xC5,0x72,0x28,0x5F,0x60,0xBF,0x19,0x2B,0xD1,0x72,0xA2,0xB7,0xD8,0x3F,0xE0,0x97,0x34,0x5A,0x01,0xBD,0x04,0x9C,0xC8,0x78,0x45,0xCD,0x93,0x8D,0x15,0xF2,0x76,0x10,0x11,0xAB,0xB8,0x5B,0x2E,0x9E,0x52,0xDD,0x81,0x3E,0x9C,0x64,0xC8,0x29,0x93,0x02,0x01,0x03,0x30,0x0D,0x06,
			0x09,0x2A,0x86,0x48,0x86,0xF7,0x0D,0x01,0x01,0x04,0x05,0x00,0x03,0x81,0x81,0x00,0x32,0x1A,0x35,0xBA,0xBF,0x43,0x27,0xD6,0xB4,0xD4,0xB8,0x76,0xE5,0xE3,0x9B,0x4D,0x6C,0xC0,0x86,0xC9,0x77,0x35,0xBA,0x6B,0x16,0x2D,0x13,0x46,0x4A,0xB0,0x32,0x53,0xA1,0x5B,0x5A,0xE9,0x99,0xE2,0x0C,0x86,0x88,0x17,0x4E,0x0D,0xFE,0x82,0xAC,0x4E,0x47,0xEF,0xFB,0xFF,0x39,0xAC,0xEE,0x35,0xC8,0xFA,0x52,0x37,0x0A,0x49,0xAD,0x59,0xAD,0xE2,0x8A,0xA9,0x1C,0xC6,0x5F,0x1F,0xF8,0x6F,0x73,0x7E,0xCD,0xA0,0x31,0xE8,0x0C,0xBE,0xF5,0x4D,
			0xD9,0xB2,0xAB,0x8A,0x12,0xB6,0x30,0x78,0x68,0x11,0x7C,0x0D,0xF1,0x49,0x4D,0xA3,0xFD,0xB2,0xE9,0xFF,0x1D,0xF0,0x91,0xFA,0x54,0x85,0xFF,0x33,0x90,0xE8,0xC1,0xBF,0xA4,0x9B,0xA4,0x62,0x46,0xBD,0x61,0x12,0x59,0x98,0x41,0x89 };

		static byte[] cert3 = { 0x30,0x82,0x03,0x04,0x30,0x82,0x02,0xC4,0xA0,0x03,0x02,0x01,0x02,0x02,0x01,0x03,0x30,0x09,0x06,0x07,0x2A,0x86,0x48,0xCE,0x38,0x04,0x03,0x30,0x51,0x31,0x0B,0x30,0x09,0x06,0x03,0x55,0x04,0x06,0x13,0x02,0x55,0x53,0x31,0x18,0x30,0x16,0x06,0x03,0x55,0x04,0x0A,0x13,0x0F,0x55,0x2E,0x53,0x2E,0x20,0x47,0x6F,0x76,0x65,0x72,0x6E,0x6D,0x65,0x6E,0x74,0x31,0x0C,0x30,0x0A,0x06,0x03,0x55,0x04,0x0B,0x13,0x03,0x44,0x6F,0x44,0x31,0x1A,0x30,0x18,0x06,0x03,0x55,0x04,0x03,0x13,0x11,0x41,0x72,0x6D,0x65,0x64,0x20,0x46,0x6F,
			0x72,0x63,0x65,0x73,0x20,0x52,0x6F,0x6F,0x74,0x30,0x1E,0x17,0x0D,0x30,0x30,0x31,0x30,0x32,0x35,0x30,0x30,0x30,0x30,0x30,0x30,0x5A,0x17,0x0D,0x30,0x33,0x30,0x31,0x30,0x31,0x30,0x30,0x30,0x30,0x30,0x30,0x5A,0x30,0x51,0x31,0x0B,0x30,0x09,0x06,0x03,0x55,0x04,0x06,0x13,0x02,0x55,0x53,0x31,0x18,0x30,0x16,0x06,0x03,0x55,0x04,0x0A,0x13,0x0F,0x55,0x2E,0x53,0x2E,0x20,0x47,0x6F,0x76,0x65,0x72,0x6E,0x6D,0x65,0x6E,0x74,0x31,0x0C,0x30,0x0A,0x06,0x03,0x55,0x04,0x0B,0x13,0x03,0x44,0x6F,0x44,0x31,0x1A,0x30,0x18,
			0x06,0x03,0x55,0x04,0x03,0x13,0x11,0x41,0x72,0x6D,0x65,0x64,0x20,0x46,0x6F,0x72,0x63,0x65,0x73,0x20,0x52,0x6F,0x6F,0x74,0x30,0x82,0x01,0xB6,0x30,0x82,0x01,0x2B,0x06,0x07,0x2A,0x86,0x48,0xCE,0x38,0x04,0x01,0x30,0x82,0x01,0x1E,0x02,0x81,0x81,0x00,0x90,0x89,0x3E,0x18,0x1B,0xFE,0xA3,0x1D,0x16,0x89,0x00,0xB4,0xD5,0x40,0x82,0x4C,0x2E,0xEC,0x3D,0x66,0x0D,0x0D,0xB9,0x17,0x40,0x6E,0x3A,0x5C,0x03,0x7B,0x1B,0x93,0x28,0x0C,0xEF,0xB9,0x97,0xE3,0xA1,0xEB,0xE2,0xA3,0x7C,0x61,0xDD,0x6F,0xD5,0xAD,0x15,0x69,0x00,
			0x16,0xB2,0xC3,0x08,0x3D,0xC4,0x59,0xC6,0xF2,0x70,0xA5,0xB0,0xF5,0x1F,0x1D,0xF4,0xB0,0x15,0xDA,0x7E,0x28,0x39,0x24,0x99,0x36,0x5B,0xEC,0x39,0x25,0xFA,0x92,0x49,0x65,0xD2,0x43,0x05,0x6A,0x9E,0xA3,0x7B,0xF0,0xDE,0xA3,0x2F,0xD3,0x6F,0x3A,0xF9,0x35,0xC3,0x29,0xD4,0x45,0x6C,0x56,0x9A,0xDE,0x36,0x6E,0xFE,0x12,0x68,0x96,0x7B,0x45,0x1D,0x2C,0xFF,0xB9,0x2D,0xF5,0x52,0x8C,0xDF,0x3E,0x2F,0x63,0x02,0x15,0x00,0x81,0xA9,0xB5,0xD0,0x04,0xF2,0x9B,0xA7,0xD8,0x55,0x4C,0x3B,0x32,0xA1,0x45,0x32,0x4F,0xF5,0x51,0xDD,
			0x02,0x81,0x80,0x64,0x7A,0x88,0x0B,0xF2,0x3E,0x91,0x81,0x59,0x9C,0xF4,0xEA,0xC6,0x7B,0x0E,0xBE,0xEA,0x05,0xE8,0x77,0xFD,0x20,0x34,0x87,0xA1,0xC4,0x69,0xF6,0xC8,0x8B,0x19,0xDA,0xCD,0xFA,0x21,0x8A,0x57,0xA9,0x7A,0x26,0x0A,0x56,0xD4,0xED,0x4B,0x1B,0x7C,0x70,0xED,0xB4,0xE6,0x7A,0x6A,0xDE,0xD3,0x29,0xE2,0xE9,0x9A,0x33,0xED,0x09,0x8D,0x9E,0xDF,0xDA,0x2E,0x4A,0xC1,0x50,0x92,0xEE,0x2F,0xE5,0x5A,0xF3,0x85,0x62,0x6A,0x48,0xDC,0x1B,0x02,0x98,0xA6,0xB0,0xD1,0x09,0x4B,0x10,0xD1,0xF0,0xFA,0xE0,0xB1,0x1D,0x13,
			0x54,0x4B,0xC0,0xA8,0x40,0xEF,0x71,0xE8,0x56,0x6B,0xA2,0x29,0xCB,0x1E,0x09,0x7D,0x27,0x39,0x91,0x3B,0x20,0x4F,0x98,0x39,0xE8,0x39,0xCA,0x98,0xC5,0xAF,0x54,0x03,0x81,0x84,0x00,0x02,0x81,0x80,0x54,0xA8,0x88,0xB5,0x8F,0x01,0x56,0xCE,0x18,0x8F,0xA6,0xD6,0x7C,0x29,0x29,0x75,0x45,0xE8,0x31,0xA4,0x07,0x17,0xED,0x1E,0x5D,0xB2,0x7B,0xBB,0xCE,0x3C,0x97,0x67,0x1E,0x88,0x0A,0xFE,0x7D,0x00,0x22,0x27,0x1D,0x66,0xEE,0xF6,0x1B,0xB6,0x95,0x7F,0x5A,0xFF,0x06,0x34,0x02,0x43,0xC3,0x83,0xC4,0x66,0x2C,0xA1,0x05,0x0E,
			0x68,0xB3,0xCA,0xDC,0xD3,0xF9,0x0C,0xC0,0x66,0xDF,0x85,0x84,0x4B,0x20,0x5D,0x41,0xAC,0xC0,0xEC,0x37,0x92,0x0E,0x97,0x19,0xBF,0x53,0x35,0x63,0x27,0x18,0x33,0x35,0x42,0x4D,0xF0,0x2D,0x6D,0xA7,0xA4,0x98,0xAA,0x57,0xF3,0xD2,0xB8,0x6E,0x4E,0x8F,0xFF,0xBE,0x6F,0x4E,0x0F,0x0B,0x44,0x24,0xEE,0xDF,0x4C,0x22,0x5B,0x44,0x98,0x94,0xCB,0xB8,0xA3,0x2F,0x30,0x2D,0x30,0x1D,0x06,0x03,0x55,0x1D,0x0E,0x04,0x16,0x04,0x14,0x9D,0x2D,0x73,0xC3,0xB8,0xE3,0x4D,0x29,0x28,0xC3,0x65,0xBE,0xA9,0x98,0xCB,0xD6,0x8A,0x06,0x68,
			0x9C,0x30,0x0C,0x06,0x03,0x55,0x1D,0x13,0x04,0x05,0x30,0x03,0x01,0x01,0xFF,0x30,0x09,0x06,0x07,0x2A,0x86,0x48,0xCE,0x38,0x04,0x03,0x03,0x2F,0x00,0x30,0x2C,0x02,0x14,0x5A,0x1B,0x2D,0x08,0x0E,0xE6,0x99,0x38,0x8F,0xB5,0x09,0xC9,0x89,0x79,0x7E,0x01,0x30,0xBD,0xCE,0xF0,0x02,0x14,0x71,0x7B,0x08,0x51,0x97,0xCE,0x4D,0x1F,0x6A,0x84,0x47,0x3A,0xC0,0xBD,0x13,0x89,0x81,0xB9,0x01,0x97 };

		static byte[] x509crl = { 0x30, 0x82, 0x01, 0x05, 0x30, 0x72, 0x02, 0x01, 0x01, 0x30, 0x0B, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x05, 0x30, 0x51, 0x31, 0x0B, 0x30, 0x09, 0x06, 0x03, 0x55, 0x04, 0x06, 0x13, 0x02, 0x55, 0x53, 0x31, 0x18, 0x30, 0x16, 0x06, 0x03, 0x55, 0x04, 0x0A, 0x13, 0x0F, 0x55, 0x2E, 0x53, 0x2E, 0x20, 0x47, 0x6F, 0x76, 0x65, 0x72, 0x6E, 0x6D, 0x65, 0x6E, 0x74, 0x31, 0x0C, 0x30, 0x0A, 0x06, 0x03, 0x55, 0x04, 0x0B, 0x13, 0x03, 0x44, 0x6F, 0x44, 0x31, 0x1A, 0x30, 0x18, 0x06, 0x03, 0x55, 0x04, 0x03, 0x13, 0x11, 0x41, 0x72, 0x6D, 0x65, 0x64, 0x20, 0x46, 0x6F, 0x72, 0x63, 0x65, 0x73, 0x20, 0x52, 0x6F, 0x6F, 0x74, 0x17, 0x0D, 0x30, 0x32, 0x31, 0x30, 0x31, 0x31, 0x31, 0x33, 0x31, 0x32, 0x35, 0x30, 0x5A, 0x30, 0x0B, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x05, 0x03, 0x81, 0x81, 0x00, 0x7D, 0xA2, 0xD1, 0x19, 0x6D, 0x0F, 0x0F, 0xCB, 0xE4, 0xA3, 0xBE, 0xE0, 0x36, 0x0A, 0xF3, 0x4C, 0x9B, 0xAF, 0xE6, 0x4F, 0xF6, 0xE3, 0xAF, 0xCF, 0x55, 0xF3, 0xC6, 0xDB, 0xAB, 0x4C, 0x16, 0x32, 0xAA, 0x73, 0xAD, 0xCC, 0xDC, 0x32, 0x33, 0x60, 0xDF, 0x8B, 0xCC, 0x93, 0xB5, 0x4F, 0x6A, 0xEC, 0x70, 0x53, 0xAF, 0xCF, 0x07, 0x0F, 0xA0, 0xCD, 0x66, 0xAC, 0x00, 0x57, 0xC6, 0x5C, 0x5D, 0x21, 0xB1, 0xBD, 0x30, 0x89, 0x8E, 0x77, 0x8D, 0xD4, 0x69, 0x7E, 0xC0, 0x36, 0x7E, 0xD2, 0xD8, 0x20, 0x71, 0x08, 0x80, 0xD2, 0xCB, 0x74, 0x8B, 0xD8, 0x42, 0x17, 0x04, 0x99, 0x80, 0xA4, 0x52, 0x70, 0x2E, 0xC0, 0xE3, 0x8C, 0x0B, 0xFF, 0x79, 0xB7, 0x45, 0x77, 0xDC, 0xC5, 0xCF, 0x43, 0x98, 0x91, 0x7D, 0xF1, 0x01, 0xF7, 0x53, 0xD7, 0xC6, 0x51, 0x35, 0xF0, 0x89, 0xCC, 0xC1, 0xFF, 0xE2, 0x89 };

		[Test]
		[ExpectedException (typeof (CryptographicException))]
		public void Constructor1 () 
		{
			KeyInfoX509Data data1 = new KeyInfoX509Data ();
			XmlElement invalid = data1.GetXml ();
		}

		[Test]
		public void Constructor2 () 
		{
			KeyInfoX509Data data1 = new KeyInfoX509Data ();
			KeyInfoX509Data data2 = new KeyInfoX509Data (cert);

			XmlElement xel = data2.GetXml ();
			string s = "<X509Data xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><X509Certificate>MIIJuTCCCSKgAwIBAgIQIAs1Xs7EsGO33sY0uXA0RDANBgkqhkiG9w0BAQQFADBiMREwDwYDVQQHEwhJbnRlcm5ldDEXMBUGA1UEChMOVmVyaVNpZ24sIEluYy4xNDAyBgNVBAsTK1ZlcmlTaWduIENsYXNzIDEgQ0EgLSBJbmRpdmlkdWFsIFN1YnNjcmliZXIwHhcNOTYwODIxMDAwMDAwWhcNOTcwODIwMjM1OTU5WjCCAQoxETAPBgNVBAcTCEludGVybmV0MRcwFQYDVQQKEw5WZXJpU2lnbiwgSW5jLjE0MDIGA1UECxMrVmVyaVNpZ24gQ2xhc3MgMSBDQSAtIEluZGl2aWR1YWwgU3Vic2NyaWJlcjFGMEQGA1UECxM9d3d3LnZlcmlzaWduLmNvbS9yZXBvc2l0b3J5L0NQUyBJbmNvcnAuIGJ5IFJlZi4sTElBQi5MVEQoYyk5NjEmMCQGA1UECxMdRGlnaXRhbCBJRCBDbGFzcyAxIC0gTmV0c2NhcGUxFjAUBgNVBAMTDURhdmlkIFQuIEdyYXkxHjAcBgkqhkiG9w0BCQEWD2RhdmlkQGZvcm1hbC5pZTBcMA0GCSqGSIb3DQEBAQUAA0sAMEgCQQDFgQei6w+4//j4HO4y/78SNWr5a8i+L/s+rwRRSqzdECmozUBbZh6Y7/JMd/qPhtEhZ5JESsSJyYPPiJ9v4jI1AgMBAAGjggcIMIIHBDAJBgNVHRMEAjAAMIICHwYDVR0DBIICFjCCAhIwggIOMIICCgYLYIZIAYb4RQEHAQEwggH5FoIBp1RoaXMgY2VydGlmaWNhdGUgaW5jb3Jwb3JhdGVzIGJ5IHJlZmVyZW5jZSwgYW5kIGl0cyB1c2UgaXMgc3RyaWN0bHkgc3ViamVjdCB0bywgdGhlIFZlcmlTaWduIENlcnRpZmljYXRpb24gUHJhY3RpY2UgU3RhdGVtZW50IChDUFMpLCBhdmFpbGFibGUgYXQ6IGh0dHBzOi8vd3d3LnZlcmlzaWduLmNvbS9DUFM7IGJ5IEUtbWFpbCBhdCBDUFMtcmVxdWVzdHNAdmVyaXNpZ24uY29tOyBvciBieSBtYWlsIGF0IFZlcmlTaWduLCBJbmMuLCAyNTkzIENvYXN0IEF2ZS4sIE1vdW50YWluIFZpZXcsIENBIDk0MDQzIFVTQSBUZWwuICsxICg0MTUpIDk2MS04ODMwIENvcHlyaWdodCAoYykgMTk5NiBWZXJpU2lnbiwgSW5jLiAgQWxsIFJpZ2h0cyBSZXNlcnZlZC4gQ0VSVEFJTiBXQVJSQU5USUVTIERJU0NMQUlNRUQgYW5kIExJQUJJTElUWSBMSU1JVEVELqAOBgxghkgBhvhFAQcBAQGhDgYMYIZIAYb4RQEHAQECMCwwKhYoaHR0cHM6Ly93d3cudmVyaXNpZ24uY29tL3JlcG9zaXRvcnkvQ1BTIDARBglghkgBhvhCAQEEBAMCB4AwNgYJYIZIAYb4QgEIBCkWJ2h0dHBzOi8vd3d3LnZlcmlzaWduLmNvbS9yZXBvc2l0b3J5L0NQUzCCBIcGCWCGSAGG+EIBDQSCBHgWggR0Q0FVVElPTjogV";
			s += "GhlIENvbW1vbiBOYW1lIGluIHRoaXMgQ2xhc3MgMSBEaWdpdGFsIApJRCBpcyBub3QgYXV0aGVudGljYXRlZCBieSBWZXJpU2lnbi4gSXQgbWF5IGJlIHRoZQpob2xkZXIncyByZWFsIG5hbWUgb3IgYW4gYWxpYXMuIFZlcmlTaWduIGRvZXMgYXV0aC0KZW50aWNhdGUgdGhlIGUtbWFpbCBhZGRyZXNzIG9mIHRoZSBob2xkZXIuCgpUaGlzIGNlcnRpZmljYXRlIGluY29ycG9yYXRlcyBieSByZWZlcmVuY2UsIGFuZCAKaXRzIHVzZSBpcyBzdHJpY3RseSBzdWJqZWN0IHRvLCB0aGUgVmVyaVNpZ24gCkNlcnRpZmljYXRpb24gUHJhY3RpY2UgU3RhdGVtZW50IChDUFMpLCBhdmFpbGFibGUKaW4gdGhlIFZlcmlTaWduIHJlcG9zaXRvcnkgYXQ6IApodHRwczovL3d3dy52ZXJpc2lnbi5jb207IGJ5IEUtbWFpbCBhdApDUFMtcmVxdWVzdHNAdmVyaXNpZ24uY29tOyBvciBieSBtYWlsIGF0IFZlcmlTaWduLApJbmMuLCAyNTkzIENvYXN0IEF2ZS4sIE1vdW50YWluIFZpZXcsIENBIDk0MDQzIFVTQQoKQ29weXJpZ2h0IChjKTE5OTYgVmVyaVNpZ24sIEluYy4gIEFsbCBSaWdodHMgClJlc2VydmVkLiBDRVJUQUlOIFdBUlJBTlRJRVMgRElTQ0xBSU1FRCBBTkQgCkxJQUJJTElUWSBMSU1JVEVELgoKV0FSTklORzogVEhFIFVTRSBPRiBUSElTIENFUlRJRklDQVRFIElTIFNUUklDVExZClNVQkpFQ1QgVE8gVEhFIFZFUklTSUdOIENFUlRJRklDQVRJT04gUFJBQ1RJQ0UKU1RBVEVNRU5ULiAgVEhFIElTU1VJTkcgQVVUSE9SSVRZIERJU0NMQUlNUyBDRVJUQUlOCklNUExJRUQgQU5EIEVYUFJFU1MgV0FSUkFOVElFUywgSU5DTFVESU5HIFdBUlJBTlRJRVMKT0YgTUVSQ0hBTlRBQklMSVRZIE9SIEZJVE5FU1MgRk9SIEEgUEFSVElDVUxBUgpQVVJQT1NFLCBBTkQgV0lMTCBOT1QgQkUgTElBQkxFIEZPUiBDT05TRVFVRU5USUFMLApQVU5JVElWRSwgQU5EIENFUlRBSU4gT1RIRVIgREFNQUdFUy4gU0VFIFRIRSBDUFMKRk9SIERFVEFJTFMuCgpDb250ZW50cyBvZiB0aGUgVmVyaVNpZ24gcmVnaXN0ZXJlZApub252ZXJpZmllZFN1YmplY3RBdHRyaWJ1dGVzIGV4dGVuc2lvbiB2YWx1ZSBzaGFsbCAKbm90IGJlIGNvbnNpZGVyZWQgYXMgYWNjdXJhdGUgaW5mb3JtYXRpb24gdmFsaWRhdGVkIApieSB0aGUgSUEuMA0GCSqGSIb3DQEBBAUAA4GBACs9RMcyWa7xX48/h+M+64Ew+KmW2wFCCwTvNwI/1CBhWMRKOjmz+9n4pcReM1oO+pNWL2/WYaKvpQwd4kFl80B1ZoPSWrS3VguODaEzE31Jw7EAaIN/tWbUMjL+i5pa1gFyMV2FkbyTm2VgJcYfvN1pRGLCsm9Gqy8gpW/aSGyc</X509Certificate></X509Data>";
			Assertion.AssertEquals ("1 cert", s, (data2.GetXml ().OuterXml));

			data1.LoadXml (xel);
			Assertion.AssertEquals ("data1==data2", (data1.GetXml ().OuterXml), (data2.GetXml ().OuterXml));

			X509Certificate x509 = new X509Certificate (cert);
			KeyInfoX509Data data3 = new KeyInfoX509Data (x509);
			Assertion.AssertEquals ("data2==data3", (data2.GetXml ().OuterXml), (data3.GetXml ().OuterXml));
		}

		[Test]
		public void Complex () 
		{
			KeyInfoX509Data data1 = new KeyInfoX509Data (cert);
			KeyInfoX509Data data2 = new KeyInfoX509Data ();

			XmlElement xel = data1.GetXml ();
			data2.LoadXml (xel);

			Assertion.AssertEquals ("data1==data2", (data1.GetXml ().OuterXml), (data2.GetXml ().OuterXml));
			byte[] c = (data1.Certificates[0] as X509Certificate).GetRawCertData();
			AssertCrypto.AssertEquals ("Certificate[0]", cert, c);

			// add a second X.509 certificate
			X509Certificate x509 = new X509Certificate (cert2);
			data1.AddCertificate (x509);
			xel = data1.GetXml ();
			data2.LoadXml (xel);
			Assertion.AssertEquals ("data1==data2", (data1.GetXml ().OuterXml), (data2.GetXml ().OuterXml));
			c = (data1.Certificates [1] as X509Certificate).GetRawCertData();
			AssertCrypto.AssertEquals ("Certificate[1]", cert2, c);

			// add properties from a third X.509 certificate
			x509 = new X509Certificate (cert3);
			data1.AddIssuerSerial (x509.GetIssuerName (), x509.GetSerialNumberString ());
			xel = data1.GetXml ();
			data2.LoadXml (xel);
			Assertion.AssertEquals ("data1==data2", (data1.GetXml ().OuterXml), (data2.GetXml ().OuterXml));
			// TODO: The type of IssuerSerial isn't documented

			// X509Certificate doesn't export SubjectKeyId so we must improvise
			byte[] skid = { 0xDE, 0xAD, 0xC0, 0xDE };
			data1.AddSubjectKeyId (skid);
			xel = data1.GetXml ();
			data2.LoadXml (xel);
			Assertion.AssertEquals ("data1==data2", (data1.GetXml ().OuterXml), (data2.GetXml ().OuterXml));
			AssertCrypto.AssertEquals ("SubjectKeyId", skid, (byte[]) data1.SubjectKeyIds[0]);

			data1.AddSubjectName (x509.GetName ());
			xel = data1.GetXml ();
			data2.LoadXml (xel);
			Assertion.AssertEquals ("data1==data2", (data1.GetXml ().OuterXml), (data2.GetXml ().OuterXml));
			string s = (string) data1.SubjectNames [0];
			Assertion.AssertEquals ("SubjectName", x509.GetName (), s);
		}

		// There's a bug in the framework 1.0 where a CRL entry cannot be included
		// in a same X509Data entry. This is not per XMLDSIG spec so Mono will accept
		// this (anyway the bug should be fixed by Microsoft in future release).
		[Test]
		public void CRL () 
		{
			KeyInfoX509Data data1 = new KeyInfoX509Data ();
			data1.CRL = x509crl;
			XmlElement xel = data1.GetXml ();

			KeyInfoX509Data data2 = new KeyInfoX509Data ();
			data2.LoadXml (xel);

			Assertion.AssertEquals ("data1==data2", (data1.GetXml ().OuterXml), (data2.GetXml ().OuterXml));
			AssertCrypto.AssertEquals ("crl1==crl2", data1.CRL, data2.CRL);
		}

		[Test]
		public void ImportX509Data () 
		{
			string simple = "<X509Data xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><X509Certificate>MIIJuTCCCSKgAwIBAgIQIAs1Xs7EsGO33sY0uXA0RDANBgkqhkiG9w0BAQQFADBiMREwDwYDVQQHEwhJbnRlcm5ldDEXMBUGA1UEChMOVmVyaVNpZ24sIEluYy4xNDAyBgNVBAsTK1ZlcmlTaWduIENsYXNzIDEgQ0EgLSBJbmRpdmlkdWFsIFN1YnNjcmliZXIwHhcNOTYwODIxMDAwMDAwWhcNOTcwODIwMjM1OTU5WjCCAQoxETAPBgNVBAcTCEludGVybmV0MRcwFQYDVQQKEw5WZXJpU2lnbiwgSW5jLjE0MDIGA1UECxMrVmVyaVNpZ24gQ2xhc3MgMSBDQSAtIEluZGl2aWR1YWwgU3Vic2NyaWJlcjFGMEQGA1UECxM9d3d3LnZlcmlzaWduLmNvbS9yZXBvc2l0b3J5L0NQUyBJbmNvcnAuIGJ5IFJlZi4sTElBQi5MVEQoYyk5NjEmMCQGA1UECxMdRGlnaXRhbCBJRCBDbGFzcyAxIC0gTmV0c2NhcGUxFjAUBgNVBAMTDURhdmlkIFQuIEdyYXkxHjAcBgkqhkiG9w0BCQEWD2RhdmlkQGZvcm1hbC5pZTBcMA0GCSqGSIb3DQEBAQUAA0sAMEgCQQDFgQei6w+4//j4HO4y/78SNWr5a8i+L/s+rwRRSqzdECmozUBbZh6Y7/JMd/qPhtEhZ5JESsSJyYPPiJ9v4jI1AgMBAAGjggcIMIIHBDAJBgNVHRMEAjAAMIICHwYDVR0DBIICFjCCAhIwggIOMIICCgYLYIZIAYb4RQEHAQEwggH5FoIBp1RoaXMgY2VydGlmaWNhdGUgaW5jb3Jwb3JhdGVzIGJ5IHJlZmVyZW5jZSwgYW5kIGl0cyB1c2UgaXMgc3RyaWN0bHkgc3ViamVjdCB0bywgdGhlIFZlcmlTaWduIENlcnRpZmljYXRpb24gUHJhY3RpY2UgU3RhdGVtZW50IChDUFMpLCBhdmFpbGFibGUgYXQ6IGh0dHBzOi8vd3d3LnZlcmlzaWduLmNvbS9DUFM7IGJ5IEUtbWFpbCBhdCBDUFMtcmVxdWVzdHNAdmVyaXNpZ24uY29tOyBvciBieSBtYWlsIGF0IFZlcmlTaWduLCBJbmMuLCAyNTkzIENvYXN0IEF2ZS4sIE1vdW50YWluIFZpZXcsIENBIDk0MDQzIFVTQSBUZWwuICsxICg0MTUpIDk2MS04ODMwIENvcHlyaWdodCAoYykgMTk5NiBWZXJpU2lnbiwgSW5jLiAgQWxsIFJpZ2h0cyBSZXNlcnZlZC4gQ0VSVEFJTiBXQVJSQU5USUVTIERJU0NMQUlNRUQgYW5kIExJQUJJTElUWSBMSU1JVEVELqAOBgxghkgBhvhFAQcBAQGhDgYMYIZIAYb4RQEHAQECMCwwKhYoaHR0cHM6Ly93d3cudmVyaXNpZ24uY29tL3JlcG9zaXRvcnkvQ1BTIDARBglghkgBhvhCAQEEBAMCB4AwNgYJYIZIAYb4QgEIBCkWJ2h0dHBzOi8vd3d3LnZlcmlzaWduLmNvbS9yZXBvc2l0b3J5L0NQUzCCBIcGCWCGSAGG+EIBDQSCBHgWggR0Q0FVVElPTjogV";
			simple += "GhlIENvbW1vbiBOYW1lIGluIHRoaXMgQ2xhc3MgMSBEaWdpdGFsIApJRCBpcyBub3QgYXV0aGVudGljYXRlZCBieSBWZXJpU2lnbi4gSXQgbWF5IGJlIHRoZQpob2xkZXIncyByZWFsIG5hbWUgb3IgYW4gYWxpYXMuIFZlcmlTaWduIGRvZXMgYXV0aC0KZW50aWNhdGUgdGhlIGUtbWFpbCBhZGRyZXNzIG9mIHRoZSBob2xkZXIuCgpUaGlzIGNlcnRpZmljYXRlIGluY29ycG9yYXRlcyBieSByZWZlcmVuY2UsIGFuZCAKaXRzIHVzZSBpcyBzdHJpY3RseSBzdWJqZWN0IHRvLCB0aGUgVmVyaVNpZ24gCkNlcnRpZmljYXRpb24gUHJhY3RpY2UgU3RhdGVtZW50IChDUFMpLCBhdmFpbGFibGUKaW4gdGhlIFZlcmlTaWduIHJlcG9zaXRvcnkgYXQ6IApodHRwczovL3d3dy52ZXJpc2lnbi5jb207IGJ5IEUtbWFpbCBhdApDUFMtcmVxdWVzdHNAdmVyaXNpZ24uY29tOyBvciBieSBtYWlsIGF0IFZlcmlTaWduLApJbmMuLCAyNTkzIENvYXN0IEF2ZS4sIE1vdW50YWluIFZpZXcsIENBIDk0MDQzIFVTQQoKQ29weXJpZ2h0IChjKTE5OTYgVmVyaVNpZ24sIEluYy4gIEFsbCBSaWdodHMgClJlc2VydmVkLiBDRVJUQUlOIFdBUlJBTlRJRVMgRElTQ0xBSU1FRCBBTkQgCkxJQUJJTElUWSBMSU1JVEVELgoKV0FSTklORzogVEhFIFVTRSBPRiBUSElTIENFUlRJRklDQVRFIElTIFNUUklDVExZClNVQkpFQ1QgVE8gVEhFIFZFUklTSUdOIENFUlRJRklDQVRJT04gUFJBQ1RJQ0UKU1RBVEVNRU5ULiAgVEhFIElTU1VJTkcgQVVUSE9SSVRZIERJU0NMQUlNUyBDRVJUQUlOCklNUExJRUQgQU5EIEVYUFJFU1MgV0FSUkFOVElFUywgSU5DTFVESU5HIFdBUlJBTlRJRVMKT0YgTUVSQ0hBTlRBQklMSVRZIE9SIEZJVE5FU1MgRk9SIEEgUEFSVElDVUxBUgpQVVJQT1NFLCBBTkQgV0lMTCBOT1QgQkUgTElBQkxFIEZPUiBDT05TRVFVRU5USUFMLApQVU5JVElWRSwgQU5EIENFUlRBSU4gT1RIRVIgREFNQUdFUy4gU0VFIFRIRSBDUFMKRk9SIERFVEFJTFMuCgpDb250ZW50cyBvZiB0aGUgVmVyaVNpZ24gcmVnaXN0ZXJlZApub252ZXJpZmllZFN1YmplY3RBdHRyaWJ1dGVzIGV4dGVuc2lvbiB2YWx1ZSBzaGFsbCAKbm90IGJlIGNvbnNpZGVyZWQgYXMgYWNjdXJhdGUgaW5mb3JtYXRpb24gdmFsaWRhdGVkIApieSB0aGUgSUEuMA0GCSqGSIb3DQEBBAUAA4GBACs9RMcyWa7xX48/h+M+64Ew+KmW2wFCCwTvNwI/1CBhWMRKOjmz+9n4pcReM1oO+pNWL2/WYaKvpQwd4kFl80B1ZoPSWrS3VguODaEzE31Jw7EAaIN/tWbUMjL+i5pa1gFyMV2FkbyTm2VgJcYfvN1pRGLCsm9Gqy8gpW/aSGyc</X509Certificate></X509Data>";
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (simple);

			KeyInfoX509Data data1 = new KeyInfoX509Data ();
			data1.LoadXml (doc.DocumentElement);

			// verify that proper XML is generated (equals to original)
			string s = (data1.GetXml ().OuterXml);
			Assertion.AssertEquals ("Xml-Simple", simple, s);

			// verify that property is parsed correctly
			byte[] c = (data1.Certificates[0] as X509Certificate).GetRawCertData();
			AssertCrypto.AssertEquals ("Certificate[0]", cert, c);

			string complex = "<X509Data xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><X509IssuerSerial><X509IssuerName>C=US, O=U.S. Government, OU=DoD, CN=Armed Forces Root</X509IssuerName><X509SerialNumber>03</X509SerialNumber></X509IssuerSerial><X509SKI>3q3A3g==</X509SKI><X509SubjectName>C=US, O=U.S. Government, OU=DoD, CN=Armed Forces Root</X509SubjectName><X509Certificate>MIIJuTCCCSKgAwIBAgIQIAs1Xs7EsGO33sY0uXA0RDANBgkqhkiG9w0BAQQFADBiMREwDwYDVQQHEwhJbnRlcm5ldDEXMBUGA1UEChMOVmVyaVNpZ24sIEluYy4xNDAyBgNVBAsTK1ZlcmlTaWduIENsYXNzIDEgQ0EgLSBJbmRpdmlkdWFsIFN1YnNjcmliZXIwHhcNOTYwODIxMDAwMDAwWhcNOTcwODIwMjM1OTU5WjCCAQoxETAPBgNVBAcTCEludGVybmV0MRcwFQYDVQQKEw5WZXJpU2lnbiwgSW5jLjE0MDIGA1UECxMrVmVyaVNpZ24gQ2xhc3MgMSBDQSAtIEluZGl2aWR1YWwgU3Vic2NyaWJlcjFGMEQGA1UECxM9d3d3LnZlcmlzaWduLmNvbS9yZXBvc2l0b3J5L0NQUyBJbmNvcnAuIGJ5IFJlZi4sTElBQi5MVEQoYyk5NjEmMCQGA1UECxMdRGlnaXRhbCBJRCBDbGFzcyAxIC0gTmV0c2NhcGUxFjAUBgNVBAMTDURhdmlkIFQuIEdyYXkxHjAcBgkqhkiG9w0BCQEWD2RhdmlkQGZvcm1hbC5pZTBcMA0GCSqGSIb3DQEBAQUAA0sAMEgCQQDFgQei6w+4//j4HO4y/78SNWr5a8i+L/s+rwRRSqzdECmozUBbZh6Y7/JMd/qPhtEhZ5JESsSJyYPPiJ9v4jI1AgMBAAGjggcIMIIHBDAJBgNVHRMEAjAAMIICHwYDVR0DBIICFjCCAhIwggIOMIICCgYLYIZIAYb4RQEHAQEwggH5FoIBp1RoaXMgY2VydGlmaWNhdGUgaW5jb3Jwb3JhdGVzIGJ5IHJlZmVyZW5jZSwgYW5kIGl0cyB1c2UgaXMgc3RyaWN0bHkgc3ViamVjdCB0bywgdGhlIFZlcmlTaWduIENlcnRpZmljYXRpb24gUHJhY3RpY2UgU3RhdGVtZW50IChDUFMpLCBhdmFpbGFibGUgYXQ6IGh0dHBzOi8vd3d3LnZlcmlzaWduLmNvbS9DUFM7IGJ5IEUtbWFpbCBhdCBDUFMtcmVxdWVzdHNAdmVyaXNpZ24uY29tOyBvciBieSBtYWlsIGF0IFZlcmlTaWduLCBJbmMuLCAyNTkzIENvYXN0IEF2ZS4sIE1vdW50YWluIFZpZXcsIENBIDk0MDQzIFVTQSBUZWwuICsxICg0MTUpIDk2MS04ODMwIENvcHlyaWdodCAoYykgMTk5NiBWZXJpU2lnbiwgSW5jLiAgQWxsIFJpZ2h0cyBSZXNlcnZlZC4gQ0VSVEFJTiBXQVJSQU5USUVTIERJU0NMQUlNRUQgYW5kIExJQUJJTElUWSBMSU1JVEVELq";
			complex += "AOBgxghkgBhvhFAQcBAQGhDgYMYIZIAYb4RQEHAQECMCwwKhYoaHR0cHM6Ly93d3cudmVyaXNpZ24uY29tL3JlcG9zaXRvcnkvQ1BTIDARBglghkgBhvhCAQEEBAMCB4AwNgYJYIZIAYb4QgEIBCkWJ2h0dHBzOi8vd3d3LnZlcmlzaWduLmNvbS9yZXBvc2l0b3J5L0NQUzCCBIcGCWCGSAGG+EIBDQSCBHgWggR0Q0FVVElPTjogVGhlIENvbW1vbiBOYW1lIGluIHRoaXMgQ2xhc3MgMSBEaWdpdGFsIApJRCBpcyBub3QgYXV0aGVudGljYXRlZCBieSBWZXJpU2lnbi4gSXQgbWF5IGJlIHRoZQpob2xkZXIncyByZWFsIG5hbWUgb3IgYW4gYWxpYXMuIFZlcmlTaWduIGRvZXMgYXV0aC0KZW50aWNhdGUgdGhlIGUtbWFpbCBhZGRyZXNzIG9mIHRoZSBob2xkZXIuCgpUaGlzIGNlcnRpZmljYXRlIGluY29ycG9yYXRlcyBieSByZWZlcmVuY2UsIGFuZCAKaXRzIHVzZSBpcyBzdHJpY3RseSBzdWJqZWN0IHRvLCB0aGUgVmVyaVNpZ24gCkNlcnRpZmljYXRpb24gUHJhY3RpY2UgU3RhdGVtZW50IChDUFMpLCBhdmFpbGFibGUKaW4gdGhlIFZlcmlTaWduIHJlcG9zaXRvcnkgYXQ6IApodHRwczovL3d3dy52ZXJpc2lnbi5jb207IGJ5IEUtbWFpbCBhdApDUFMtcmVxdWVzdHNAdmVyaXNpZ24uY29tOyBvciBieSBtYWlsIGF0IFZlcmlTaWduLApJbmMuLCAyNTkzIENvYXN0IEF2ZS4sIE1vdW50YWluIFZpZXcsIENBIDk0MDQzIFVTQQoKQ29weXJpZ2h0IChjKTE5OTYgVmVyaVNpZ24sIEluYy4gIEFsbCBSaWdodHMgClJlc2VydmVkLiBDRVJUQUlOIFdBUlJBTlRJRVMgRElTQ0xBSU1FRCBBTkQgCkxJQUJJTElUWSBMSU1JVEVELgoKV0FSTklORzogVEhFIFVTRSBPRiBUSElTIENFUlRJRklDQVRFIElTIFNUUklDVExZClNVQkpFQ1QgVE8gVEhFIFZFUklTSUdOIENFUlRJRklDQVRJT04gUFJBQ1RJQ0UKU1RBVEVNRU5ULiAgVEhFIElTU1VJTkcgQVVUSE9SSVRZIERJU0NMQUlNUyBDRVJUQUlOCklNUExJRUQgQU5EIEVYUFJFU1MgV0FSUkFOVElFUywgSU5DTFVESU5HIFdBUlJBTlRJRVMKT0YgTUVSQ0hBTlRBQklMSVRZIE9SIEZJVE5FU1MgRk9SIEEgUEFSVElDVUxBUgpQVVJQT1NFLCBBTkQgV0lMTCBOT1QgQkUgTElBQkxFIEZPUiBDT05TRVFVRU5USUFMLApQVU5JVElWRSwgQU5EIENFUlRBSU4gT1RIRVIgREFNQUdFUy4gU0VFIFRIRSBDUFMKRk9SIERFVEFJTFMuCgpDb250ZW50cyBvZiB0aGUgVmVyaVNpZ24gcmVnaXN0ZXJlZApub252ZXJpZmllZFN1YmplY3RBdHRyaWJ1dGVzIGV4dGVuc2lvbiB2YWx1ZSBzaGFsbCAKbm90IGJlIGNvbnNpZGVyZWQgYXMgYWNjdXJhdGUgaW5mb3JtYXRpb24gdmF";
			complex += "saWRhdGVkIApieSB0aGUgSUEuMA0GCSqGSIb3DQEBBAUAA4GBACs9RMcyWa7xX48/h+M+64Ew+KmW2wFCCwTvNwI/1CBhWMRKOjmz+9n4pcReM1oO+pNWL2/WYaKvpQwd4kFl80B1ZoPSWrS3VguODaEzE31Jw7EAaIN/tWbUMjL+i5pa1gFyMV2FkbyTm2VgJcYfvN1pRGLCsm9Gqy8gpW/aSGyc</X509Certificate><X509Certificate>MIICHTCCAYYCARQwDQYJKoZIhvcNAQEEBQAwWDELMAkGA1UEBhMCQ0ExHzAdBgNVBAMTFktleXdpdG5lc3MgQ2FuYWRhIEluYy4xKDAmBgorBgEEASoCCwIBExhrZXl3aXRuZXNzQGtleXdpdG5lc3MuY2EwHhcNOTYwNTA3MDAwMDAwWhcNOTkwNTA3MDAwMDAwWjBYMQswCQYDVQQGEwJDQTEfMB0GA1UEAxMWS2V5d2l0bmVzcyBDYW5hZGEgSW5jLjEoMCYGCisGAQQBKgILAgETGGtleXdpdG5lc3NAa2V5d2l0bmVzcy5jYTCBnTANBgkqhkiG9w0BAQEFAAOBiwAwgYcCgYEAzSP6KuHtmPTp0JM+13qAAkzMwQKvXLYff/pXQm8w0SDFtSEHQCyphsLzZISuPYUu7YW9VLAYKO9q+BvnCxYfkyVPx/iOw7nKmIQOVdAv73h3xXIoX2C/GSvRcqK32D/glzRaAb0EnMh4Rc2TjRXydhARq7hbLp5S3YE+nGTIKZMCAQMwDQYJKoZIhvcNAQEEBQADgYEAMho1ur9DJ9a01Lh25eObTWzAhsl3NbprFi0TRkqwMlOhW1rpmeIMhogXTg3+gqxOR+/7/zms7jXI+lI3CkmtWa3iiqkcxl8f+G9zfs2gMegMvvVN2bKrihK2MHhoEXwN8UlNo/2y6f8d8JH6VIX/M5Dowb+km6RiRr1hElmYQYk=</X509Certificate></X509Data>";
			doc.LoadXml (complex);
			KeyInfoX509Data data2 = new KeyInfoX509Data ();
			data2.LoadXml (doc.DocumentElement);
			s = (data2.GetXml ().OuterXml);
			Assertion.AssertEquals ("Xml-Complex", complex, s);

			string crl = "<X509Data xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><X509CRL>HoIBBTByAgEBMAsGCSqGSIb3DQEBBTBRMQswCQYDVQQGEwJVUzEYMBYGA1UEChMPVS5TLiBHb3Zlcm5tZW50MQwwCgYDVQQLEwNEb0QxGjAYBgNVBAMTEUFybWVkIEZvcmNlcyBSb290Fw0wMjEwMTExMzEyNTBaMAsGCSqGSIb3DQEBBQOBgQB9otEZbQ8Py+SjvuA2CvNMm6/mT/bjr89V88bbq0wWMqpzrczcMjNg34vMk7VPauxwU6/PBw+gzWasAFfGXF0hsb0wiY53jdRpfsA2ftLYIHEIgNLLdIvYQhcEmYCkUnAuwOOMC/95t0V33MXPQ5iRffEB91PXxlE18InMwf/iiQ==</X509CRL></X509Data>";
			doc.LoadXml (crl);
			KeyInfoX509Data data3 = new KeyInfoX509Data ();
			data3.LoadXml (doc.DocumentElement);
			s = (data3.GetXml ().OuterXml);
			Assertion.AssertEquals ("Xml-Crl", crl, s);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void InvalidKeyNode1 () 
		{
			KeyInfoX509Data data1 = new KeyInfoX509Data ();
			data1.LoadXml (null);
		}

		[Test]
		public void InvalidKeyNode2 () 
		{
			string bad = "<Test></Test>";
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (bad);
		}

		[Test]
		[ExpectedException (typeof (CryptographicException))]
		public void InvalidKeyNode3 () 
		{
			string bad = "<Test></Test>";
			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (bad);
			KeyInfoX509Data data1 = new KeyInfoX509Data ();
			data1.LoadXml (doc.DocumentElement);
		}
	}
}