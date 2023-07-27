// components/About.js
import React from 'react';
import styled from 'styled-components';

const AboutContainer = styled.div`
  max-width: 800px;
  margin: 0 auto;
  padding: 40px;
`;

const Heading = styled.h1`
  font-size: 32px;
  margin-bottom: 20px;
`;

const SubHeading = styled.h2`
  font-size: 24px;
  margin-bottom: 10px;
`;

const Paragraph = styled.p`
  font-size: 18px;
  line-height: 1.6;
  margin-bottom: 20px;
`;

const Highlight = styled.span`
  background-color: #f2f2f2;
  padding: 2px 4px;
  border-radius: 4px;
`;

export const About = ({ isLoggedIn, handleLogout }) => {
  return (
    <div>
    <AboutContainer>
      <Heading>About Our Website</Heading>
      <Paragraph>
        Welcome to Code Snippet Manager, your one-stop platform for sharing and discovering coding snippets from various programming languages and technologies.
      </Paragraph>
      <SubHeading>Our Mission</SubHeading>
      <Paragraph>
        At Code Snippet Manager, we aim to create a thriving community of developers, where they can share and explore useful code snippets to improve their productivity and enhance their coding skills.
      </Paragraph>
      <SubHeading>Why Choose Code Snippet Manager?</SubHeading>
      <Paragraph>
        <Highlight>Rich Collection of Snippets:</Highlight> Our platform hosts an extensive collection of code snippets covering a wide range of topics, from basic algorithms to advanced techniques.
      </Paragraph>
      <Paragraph>
        <Highlight>Community-Driven:</Highlight> Code Snippet Manager is driven by the community, and anyone can contribute their own code snippets to help others and gain recognition for their expertise.
      </Paragraph>
      <Paragraph>
        <Highlight>User-Friendly Interface:</Highlight> We believe in providing an intuitive and seamless experience for our users to easily find, share, and interact with snippets.
      </Paragraph>
      <Paragraph>
        <Highlight>Learning and Growth:</Highlight> Code Snippet Manager is not just a platform for sharing code; it's also a place for developers to learn from each other and grow together as a community.
      </Paragraph>
      <SubHeading>Contact Us</SubHeading>
      <Paragraph>
        Have any questions or feedback? Feel free to reach out to us at contact@CodeSnippetManager.com, and we'll get back to you as soon as possible.
      </Paragraph>
    </AboutContainer>
    <footer class="py-5 bg-dark">
            <div class="container px-4 px-lg-5"><p class="m-0 text-center text-white">Copyright &copy; Your Website 2023</p></div>
        </footer>
    </div>
  );
};

export default About;
