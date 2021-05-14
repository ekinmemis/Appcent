using System;
using System.Reflection;

using NUnit.Framework;

namespace Nop.Tests
{
    public class ExceptionAssert
    {
        public delegate void ExceptionDelegate();


        public static Exception Throws(Type exceptionType, ExceptionDelegate method)
        {
            try
            {
                method.Invoke();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exceptionType, ex.GetType());
                return ex;
            }
            Assert.Fail("Expected exception '" + exceptionType.FullName + "' wasn't thrown.");
            return null;
        }


        public static T Throws<T>(ExceptionDelegate method)
            where T : Exception
        {
            try
            {
                method.Invoke();
            }
            catch (TargetInvocationException ex)
            {
                Assert.That(ex.InnerException, Is.TypeOf(typeof(T)));
            }
            catch (T ex)
            {
                return ex;
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected exception '" + typeof(T).FullName + "' but got exception '" + ex.GetType() + "'.");
                return null;
            }
            Assert.Fail("Expected exception '" + typeof(T).FullName + "' wasn't thrown.");
            return null;
        }


        public static void InnerException<T>(ExceptionDelegate method)
            where T : Exception
        {
            try
            {
                method.Invoke();
            }
            catch (Exception ex)
            {
                TypeAssert.AreEqual(typeof(T), ex.InnerException);
                return;
            }
            Assert.Fail("Expected exception '" + typeof(T).FullName + "' wasn't thrown.");
        }
    }

    [TestFixture]
    public class ExceptionAssertTests
    {
        [Test]
        public void PassesOnExceptionTrown()
        {
            ExceptionAssert.Throws(
                typeof(ArgumentException),
                delegate
                {
                    throw new ArgumentException("catch me");
                });
        }
        [Test]
        public void ReturnsTheException()
        {
            var ex = ExceptionAssert.Throws(
                typeof(ArgumentException),
                delegate
                {
                    throw new ArgumentException("return me");
                });
            Assert.AreEqual("return me", ex.Message);
        }
        [Test]
        public void PassesOnExceptionTrown_generic()
        {
            ExceptionAssert.Throws<ArgumentException>(
                delegate
                {
                    throw new ArgumentException("catch me");
                });
        }
        [Test]
        public void ReturnsTheException_generic()
        {
            var ex = ExceptionAssert.Throws<ArgumentException>(
                delegate
                {
                    throw new ArgumentException("return me");
                });
            Assert.AreEqual("return me", ex.Message);
        }
    }
}
