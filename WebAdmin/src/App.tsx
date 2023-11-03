import { AppProvider } from './providers';
import { AppRoutes } from './routes';
import './App.css';

export const App: React.FC = () => {
  return (
    <AppProvider>
      <AppRoutes />
    </AppProvider>
  );
};
