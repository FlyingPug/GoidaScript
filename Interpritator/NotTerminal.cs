using System;
using System.Collections.Generic;
public abstract class AbstractSyntaxTree
{
    public abstract void Accept(AbstractSyntaxTreeVisitor visitor);
}
public class Program : AbstractSyntaxTree
{
    public List<Statement> Statements { get; set; }
    public Program(List<Statement> statements)
    {
        Statements = statements;
    }
    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitProgram(this);
    }
}
public class Statement : AbstractSyntaxTree
{
    public Expression Expression { get; set; }
    public Statement(Expression expression)
    {
        Expression = expression;
    }
    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitStatement(this);
    }
}
public class Expression : AbstractSyntaxTree
{
    public Token Token { get; set; }
    public Expression(Token token)
    {
        Token = token;
    }
    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitExpression(this);
    }
}
public class Token
{
    public string Value { get; set; }
    public Token(string value)
    {
        Value = value;
    }
}
public abstract class AbstractSyntaxTreeVisitor
{
    public virtual void VisitProgram(Program program) { }
    public virtual void VisitStatement(Statement statement) { }
    public virtual void VisitExpression(Expression expression) { }
    public virtual void VisitFunctionCall(FunctionCall functionCall) { }
    public virtual void VisitFunction(Function function) { }
    public virtual void VisitVariableAssignment(VariableAssignment variableAssignment) { }
    public virtual void VisitVariableDeclaration(VariableDeclaration variableDeclaration) { }
    public virtual void VisitIfStatement(IfStatement ifStatement) { }
    public virtual void VisitWhileStatement(WhileStatement whileStatement) { }
    public virtual void VisitBlock(Block block) { }
}
public class PrintVisitor : AbstractSyntaxTreeVisitor
{
    public override void VisitProgram(Program
        program)
    {
        Console.WriteLine("Program:");
        foreach (var statement in program.Statements)
        {
            statement.Accept(this);
        }
    }
    public override void VisitStatement(Statement statement)
    {
        Console.WriteLine("Statement:");
        statement.Expression.Accept(this);
    }
    public override void VisitExpression(Expression expression)
    {
        Console.WriteLine("Expression: " + expression.Token.Value);
    }
    public override void VisitFunctionCall(FunctionCall functionCall)
    {
        Console.WriteLine("FunctionCall: " + functionCall.FunctionName + "(" + string.Join(", ", functionCall.Arguments) + ")");
    }
    public override void VisitFunction(Function function)
    {
        Console.WriteLine("Function: " + function.Name + "(" + string.Join(", ", function.Parameters) + ")");
    }
    public override void VisitVariableAssignment(VariableAssignment variableAssignment)
    {
        Console.WriteLine("VariableAssignment: " + variableAssignment.VariableName + " = " + variableAssignment.Expression);
    }
    public override void VisitVariableDeclaration(VariableDeclaration variableDeclaration)
    {
        Console.WriteLine("VariableDeclaration: " + variableDeclaration.VariableName + " " + variableDeclaration.Type);
    }
    public override void VisitIfStatement(IfStatement ifStatement)
    {
        Console.WriteLine("IfStatement: if (" + ifStatement.Condition + ") " + ifStatement.ThenBlock);
        if (ifStatement.ElseBlock != null)
        {
            Console.WriteLine("ElseBlock: " + ifStatement.ElseBlock);
        }
    }
    public override void VisitWhileStatement(WhileStatement whileStatement)
    {
        Console.WriteLine("WhileStatement: while (" + whileStatement.Condition + ") " + whileStatement.Body);
    }
    public override void VisitBlock(Block block)
    {
        Console.WriteLine("Block:");
        foreach (var statement in block.Statements)
        {
            statement.Accept(this);
        }
    }
}
public class FunctionCall : AbstractSyntaxTree
{
    public string FunctionName { get; set; }
    public List<Expression> Arguments { get; set; }
    public FunctionCall(string functionName, List<Expression> arguments)
    {
        FunctionName = functionName;
        Arguments = arguments;
    }
    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitFunctionCall(this);
    }
}
public class Function : AbstractSyntaxTree
{
    public string Name { get; set; }
    public List<string> Parameters { get; set; }
    public Function(string name, List<string> parameters)
    {
        Name = name;
        Parameters = parameters;
    }
    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitFunction(this);
    }
}
public class VariableAssignment : AbstractSyntaxTree
{
    public string VariableName { get; set; }
    public Expression Expression { get; set; }
    public VariableAssignment(string variableName, Expression expression)
    {
        VariableName = variableName;
        Expression = expression;
    }
    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitVariableAssignment(this);
    }
}
public class VariableDeclaration : AbstractSyntaxTree
{
    public string VariableName { get; set; }
    public string Type { get; set; }
    public VariableDeclaration(string variableName, string type)
    {
        VariableName = variableName;
        Type = type;
    }
    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitVariableDeclaration(this);
    }
}
public class IfStatement : AbstractSyntaxTree
{
    public Expression Condition { get; set; }
    public Block ThenBlock { get; set; }
    public Block ElseBlock { get; set; }
    public IfStatement(Expression condition, Block thenBlock, Block elseBlock = null)
    {
        Condition = condition;
        ThenBlock = thenBlock;
        ElseBlock = elseBlock;
    }
    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitIfStatement(this);
    }
}
public class WhileStatement : AbstractSyntaxTree
{
    public Expression Condition { get; set; }
    public Block Body { get; set; }
    public WhileStatement(Expression condition, Block body)
    {
        Condition = condition;
        Body = body;
    }
    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitWhileStatement(this);
    }
}
public class Block : AbstractSyntaxTree
{
    public List<Statement> Statements { get; set; }
    public Block(List<Statement> statements)
    {
        Statements = statements;
    }
    public override void Accept(AbstractSyntaxTreeVisitor visitor)
    {
        visitor.VisitBlock(this);
    }
}
class Program
{
    static void Main(string[] args)
    {
        var program = new Program(new List<Statement>
{
new Statement(new Expression(new Token("1 + 2"))),
new Statement(new Expression(new Token("3 * 4"))),
new Statement(new FunctionCall(new Token("print"), new List<Expression> { new Expression(new Token("5")) })),
new Statement(new IfStatement(new Expression(new Token("x > 0")), new Block(new List<Statement> { new Statement(new Expression(new Token("print")) }))),
new Block(new List<Statement> { new Statement(new Expression(new Token("print")) }))),
    new Statement(new WhileStatement(new Expression(new Token("x < 10")), new Block(new List<Statement> { new Statement(new Expression(new Token("x = x + 1")) }))),
new Block(new List<Statement> { new Statement(new Expression(new Token("print")) }))),
new Statement(new VariableAssignment(new Token("x"), new Expression(new Token("5")))),
            new Statement(new VariableDeclaration(new Token("x"), new Token("int"))),
new Statement(new Function(new Token("print"), new List<string> { new Token("x") })),
});
var visitor = new PrintVisitor();
    program.Accept(visitor);
}
}
