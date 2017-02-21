# TheGoodTheBadTheUgly
A technical test for new candidates. The idea is that they can identify whats good, and what's not so good about the code

## Background
We had a requirement to create a small utility which would take a web site url, and simply extract and return the web addresses of all links present on the page along with the text of the link. 

In addition we needed the code to be sufficiently unit tested, and while it is likely to be executed as a command line utility in the short term, it is essential that it be easy to convert into a web service in the near future.

Because we were already stretched we decided to outsource the development of this work to a remote developer (Bob) that we found on 419Eaters.com. 

**In Bobs initial submission to us he had missunderstood the requirements and implemented a filter facility that we had not asked for. We are told he has since removed this feature.**

The code you now see is his final submission, and before payment is released to him we need you to carry out a code review of his work.

In the interest of providing positive, as well as negative feedback we need you to list what is good about the code, as well as what may be wrong with it and also suggest how you would do it differently and why. You should focus your code review on (but not limit it to) the program.cs file and unit test files.

Each comment should include the following detail
* The location of the code you are commenting on (Line Number Range or class + method name)
* Brief details as to what is good or bad about the code
* Provide justification for your comment.

For example

Location | Good/Bad | Summary | Comment
------------ | ------------- | ------------- | -------------
Line 96 | Good | Regex on multiple lines | Regex is difficult to interpret at the best of times, so its good that some attempt has been made to make it clearer

It is up to you how long you give this review but please note your comments in an order that coincides with the lines of code you are commenting on and bring them with you to a meeting to go over your findings.
