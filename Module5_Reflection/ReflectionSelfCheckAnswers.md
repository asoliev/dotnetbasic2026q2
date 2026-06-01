# Reflection Self-Check Answers

1. What is reflection in .NET?

Reflection is a .NET feature that lets a program inspect metadata about itself and other types at runtime. It can examine assemblies, types, members, attributes, and more without knowing all of them at compile time.

2. What does reflection allow you to do?

Reflection allows you to discover type information at runtime, create objects dynamically, call methods, read and write fields and properties, inspect custom attributes, and build flexible frameworks that work with unknown types.

3. What are fully qualified type names?

Fully qualified type names are the complete names of types that include their namespace and type name, and sometimes nested type information and assembly details. They uniquely identify a type so it can be found and loaded correctly.

4. What examples of practical application of reflection can you imagine?

Reflection is useful for dependency injection containers, serializers, plugin systems, object mappers, test frameworks, validation libraries, and tools that load classes dynamically based on configuration.

5. Is it possible to get information about private fields/methods using reflection?

Yes, reflection can access private members if you ask for non-public binding flags. This makes it possible to inspect private fields and methods, although doing so should be used carefully because it can break encapsulation.
