# Vampire Survivor Project

Un clone de [Vampire Survivors](https://store.steampowered.com/app/1794680/Vampire_Survivors/) fait en Godot 4 avec C#. Le joueur survit le plus longtemps possible contre des vagues de zombies, accumule de l'XP et déverrouille des armes au fil des niveaux — jusqu'à affronter le boss final.

## Jouer

1. Ouvrir le projet dans **Godot 4** (avec support .NET).
2. Lancer la scène principale `vampire_main/scene/game.tscn`.
3. Appuyer sur **Espace** pour démarrer la partie.
4. Se déplacer avec les **flèches directionnelles**.
5. Les armes s'activent automatiquement.

## Mécaniques

- **XP & niveaux** : ramasser les gemmes laissées par les ennemis tués remplit la barre d'XP. Chaque montée de niveau déverrouille une nouvelle arme (dans un ordre aléatoire à chaque partie).
- **Armes** : épée, hache, gants de boxe, projectiles linéaires, projectiles circulaires, pièges — toutes s'activent automatiquement et s'empilent au fil de la progression.
- **Ennemis** : zombies normaux, zombies rapides, zombies tanks, et un boss qui invoque des renforts à intervalles réguliers.
- **Condition de victoire** : tuer le boss — l'écran devient vert et le personnage fait une animation de victoire.
- **Condition de défaite** : mourir — l'écran devient rouge et la fenêtre se ferme.

## Stack technique

- **Moteur** : Godot 4
- **Langage** : C# (.NET / Mono)
- **Patterns** : interfaces (`ICollide`, `ITakeDamage`, `ICiblable`, …), médiateur (`MedAttack`), DCM pour les entités

## Équipe

| Membre | GitHub |
|---|---|
| Eduardo Torres | [@Teduardo94](https://github.com/Teduardo94) |
| Guillaume Foisy | [@GuillaumeF21](https://github.com/GuillaumeF21) |
| Jonathan Barahona Gonzalez | [@jobsthan](https://github.com/jobsthan) |
| Mario Laframboise | [@laframboisemario19](https://github.com/laframboisemario19) |
| Ahmed Gacem | [@DemaG123](https://github.com/DemaG123) |
