import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './App.css';

const App = () => {
  const [passwordInfo, setPasswordInfo] = useState({});
  const [passwordLength, setPasswordLength] = useState(12);
  const [includeSpecialChars, setIncludeSpecialChars] = useState(true);

  useEffect(() => {
    fetchData();
  }, [includeSpecialChars]); 

  const fetchData = async () => {
    try {
      const response = await axios.get(
        `https://localhost:7297/api/password?length=${passwordLength}&includeSpecialChars=${includeSpecialChars}`
      );
      setPasswordInfo(response.data);
    } catch (error) {
      console.error('Ошибка при получении данных:', error);
    }
  };

  const generatePassword = async () => {
    fetchData(); 
  };

  const handleLengthChange = (event) => {
    const length = parseInt(event.target.value, 10);
    if (!isNaN(length) && length > 0) {
      setPasswordLength(length);
    }
  };

  const handleSpecialCharsChange = () => {
    setIncludeSpecialChars(!includeSpecialChars);
  };

  return (
    <div className="app-container">
      <h1>Password Generator</h1>
      <label>
        Password Length:
        <input
          type="number"
          value={passwordLength}
          onChange={handleLengthChange}
          min="1"
        />
      </label>
      <label>
        Include Special Characters:
        <input
          type="checkbox"
          checked={includeSpecialChars}
          onChange={handleSpecialCharsChange}
        />
      </label>
      <p className="generated-password">Generated Password: {passwordInfo.generatedPassword}</p>
      <p className="password-strength">Password Strength: {passwordInfo.passwordScore}/10</p>
      <button onClick={generatePassword}>Generate Password</button>
    </div>
  );
};

export default App;
