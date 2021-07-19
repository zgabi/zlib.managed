// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: see LICENSE for more details.

namespace Elskom.Generic.Libs
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Threading;
    using System.Threading.Tasks;
    using IDisposableGenerator;

    /// <summary>
    /// Class that provides support for zlib compression/decompression for an input stream.
    /// This is an sealed class.
    /// </summary>
    [GenerateDispose(true)]
    public sealed partial class ZlibStream : Stream
    {
        /// <summary>The underlying deflate stream.</summary>
        [DisposeField(true)]
        private DeflateStream? deflateStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZlibStream"/> class for compression.
        /// </summary>
        /// <param name="input">The input stream to decompress with.</param>
        public ZlibStream(Stream input)
            : this(input, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZlibStream"/> class for decompression.
        /// </summary>
        /// <param name="input">The input stream to decompress with.</param>
        /// <param name="keepOpen">Whether to keep the input stream open or not.</param>
        public ZlibStream(Stream input, bool keepOpen)
            => this.deflateStream = new DeflateStream(input, CompressionMode.Decompress, keepOpen);

        /// <summary>
        /// Initializes a new instance of the <see cref="ZlibStream"/> class for decompression.
        /// </summary>
        /// <param name="output">The output stream to compress to.</param>
        /// <param name="level">The compression level for the data to compress.</param>
        public ZlibStream(Stream output, ZlibCompression level)
            : this(output, level, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZlibStream"/> class for compression.
        /// </summary>
        /// <param name="output">The output stream to compress to.</param>
        /// <param name="level">The compression level for the data to compress.</param>
        /// <param name="keepOpen">Whether to keep the output stream open or not.</param>
        public ZlibStream(Stream output, ZlibCompression level, bool keepOpen)
            => this.deflateStream = new DeflateStream(output, (CompressionLevel)level, keepOpen);

        /// <summary>Gets a value indicating whether the stream supports reading.</summary>
        public override bool CanRead
            => this.deflateStream?.CanRead ?? false;

        /// <summary>Gets a value indicating whether the stream supports writing.</summary>
        public override bool CanWrite
            => this.deflateStream?.CanWrite ?? false;

        /// <summary>Gets a value indicating whether the stream supports seeking.</summary>
        public override bool CanSeek
            => false;

        /// <inheritdoc/>
        /// <remarks>This property is not supported and always throws a <see cref="NotSupportedException"/>.</remarks>
        public override long Length
            => throw new NotSupportedException();

        /// <inheritdoc/>
        /// <remarks>This property is not supported and always throws a <see cref="NotSupportedException"/>.</remarks>
        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        /// <summary>Gets a reference to the underlying stream.</summary>
        public Stream BaseStream => this.deflateStream?.BaseStream!;

        /// <summary>Flushes the internal buffers.</summary>
        public override void Flush()
        {
            this.ThrowIfClosed();
            this.deflateStream!.Flush();
        }

        /// <summary>Asynchronously clears all buffers for this stream, causes any buffered data to be written to the underlying device, and monitors cancellation requests.</summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous flush operation.</returns>
        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            this.ThrowIfClosed();
            return this.deflateStream!.FlushAsync(cancellationToken);
        }

        /// <inheritdoc/>
        /// <remarks>This property is not supported and always throws a <see cref="NotSupportedException"/>.</remarks>
        public override long Seek(long offset, SeekOrigin origin)
            => throw new NotSupportedException();

        /// <inheritdoc/>
        /// <remarks>This property is not supported and always throws a <see cref="NotSupportedException"/>.</remarks>
        public override void SetLength(long value)
            => throw new NotSupportedException();

        /// <summary>Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.</summary>
        /// <returns>The unsigned byte cast to an <see cref="int" />, or -1 if at the end of the stream.</returns>
        public override int ReadByte()
        {
            this.ThrowIfClosed();
            return this.deflateStream!.ReadByte();
        }

        /// <summary>Begins an asynchronous read operation.</summary>
        /// <param name="buffer">The byte array to read the data into.</param>
        /// <param name="offset">The byte offset in array at which to begin reading data from the stream.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="callback">An optional asynchronous callback, to be called when the read operation is complete.</param>
        /// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
        /// <returns>An object that represents the asynchronous read operation, which could still be pending.</returns>
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback? callback, object? state)
        {
            this.ThrowIfClosed();
            return this.deflateStream!.BeginRead(buffer, offset, count, callback, state);
        }

        /// <summary>Waits for the pending asynchronous read to complete.</summary>
        /// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
        /// <returns>The number of bytes that were read into the byte array.</returns>
        public override int EndRead(IAsyncResult asyncResult)
            => this.deflateStream!.EndRead(asyncResult);

        /// <summary>Reads a number of decompressed bytes into the specified byte array.</summary>
        /// <param name="buffer">The byte array to read the data into.</param>
        /// <param name="offset">The byte offset in array at which to begin reading data from the stream.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>The number of bytes that were read into the byte array.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            this.ThrowIfClosed();
            return this.deflateStream!.Read(buffer, offset, count);
        }

        /// <summary>Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.</summary>
        /// <param name="buffer">The byte array to read the data into.</param>
        /// <param name="offset">The byte offset in array at which to begin reading data from the stream.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous completion of the operation.</returns>
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            this.ThrowIfClosed();
            return this.deflateStream!.ReadAsync(buffer, offset, count, cancellationToken);
        }

        /// <summary>Begins an asynchronous write operation.</summary>
        /// <param name="buffer">The buffer to write data from.</param>
        /// <param name="offset">The byte offset in buffer to begin writing from.</param>
        /// <param name="count">The maximum number of bytes to write.</param>
        /// <param name="callback">An optional asynchronous callback, to be called when the write operation is complete.</param>
        /// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
        /// <returns>An object that represents the asynchronous write operation, which could still be pending.</returns>
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback? callback, object? state)
        {
            this.ThrowIfClosed();
            return this.deflateStream!.BeginWrite(buffer, offset, count, callback, state);
        }

        /// <summary>Ends an asynchronous write operation.</summary>
        /// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
        public override void EndWrite(IAsyncResult asyncResult)
            => this.deflateStream?.EndWrite(asyncResult);

        /// <summary>Writes compressed bytes to the underlying stream from the specified byte array.</summary>
        /// <param name="buffer">The buffer to write data from.</param>
        /// <param name="offset">The byte offset in buffer to begin writing from.</param>
        /// <param name="count">The maximum number of bytes to write.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.ThrowIfClosed();
            this.deflateStream!.Write(buffer, offset, count);
        }

        /// <summary>Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests.</summary>
        /// <param name="buffer">The buffer to write data from.</param>
        /// <param name="offset">The byte offset in buffer to begin writing from.</param>
        /// <param name="count">The maximum number of bytes to write.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous completion of the operation.</returns>
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            this.ThrowIfClosed();
            return this.deflateStream!.WriteAsync(buffer, offset, count, cancellationToken);
        }

        /// <summary>Writes a byte to the current position in the stream and advances the position within the stream by one byte.</summary>
        /// <param name="value">The byte to write to the stream.</param>
        public override void WriteByte(byte value)
        {
            this.ThrowIfClosed();
            this.deflateStream!.WriteByte(value);
        }

        /// <summary>Reads the bytes from the current stream and writes them to another stream.</summary>
        /// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
        public new void CopyTo(Stream destination)
        {
            this.ThrowIfClosed();
            this.deflateStream!.CopyTo(destination);
        }

        /// <summary>Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified buffer size and cancellation token.</summary>
        /// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
        /// <param name="bufferSize">The size, in bytes, of the buffer. This value must be greater than zero.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous copy operation.</returns>
        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
        {
            this.ThrowIfClosed();
            return this.deflateStream!.CopyToAsync(destination, bufferSize, cancellationToken);
        }

        /// <summary>Throws an <see cref="ObjectDisposedException"/>.</summary>
        private static void ThrowClosedException() =>
            throw new ObjectDisposedException(nameof(ZlibStream), "The stream is closed.");

        /// <summary>Throws an <see cref="ObjectDisposedException"/> if the stream is closed.</summary>
        private void ThrowIfClosed()
        {
            if (this.deflateStream is null)
            {
                ThrowClosedException();
            }
        }
    }
}
