# Stargate-Mission-Manager
#### Application de gestion des missions développée en C# avec base SQLite.

"Le projet Stargate est le nom de code d'un des sous-projets du département informatique de l’IUT
Robert Schuman ayant pour objet d'investiguer la réalité et les échanges potentiels, tant militaires
qu’économiques, avec les espèces extraterrestres qui vivent dans notre galaxie, plus
particulièrement la négociation de DataBaz, ce minerai qu’on ne trouve plus que sur certaines
planètes et qui fait l’objet de la convoitise toujours plus grande des industriels et politiciens terriens."

Ce projet a une interface Windows Form

---

#### L'application contient les fonctionnalités suivantes :
- Tableau de bord des missions (en cours, à venir et/ou passées).
- Création d’une nouvelle mission puis affectation des membres et définition des
objectifs (captures d’extraterrestres, négociation de dataBaz,…)
- Récapitulatif complet des informations sur une mission (membres de l’équipage,
captures, état du budget, dépenses effectuées…)
- Visualisation en mode 1 à 1 des événements survenus lors d’une mission donnée.
- Affichage des races répertoriées.
- Affichage des informations connues sur les planètes de la galaxie.
- Données statistiques.
#### Schéma de base :

![Shéma relationnel de la base Stagate.sqlite]("./Stragate.png")

---
## Fonctionnalités détaillées

### 1. Tableau de bord des missions
- Vue synthétique regroupant toutes les missions (passées, en cours, à venir)
- Affichage des informations clés : dates, chef de mission, budget, statut
- Accès rapide à l'édition d'un **PDF de bilan** pré-rempli
- Stockage et partage des données via un **DataSet local** (classe `mesDatas.cs`)

### 2. Création d'une nouvelle mission (admin uniquement)
- **Authentification** préalable avec vérification d'un mot de passe haché
- Définition des caractéristiques :
  - Chef de mission (obligatoirement un militaire)
  - Date de départ (défaut : jour courant) et date de retour prévue
  - Feuille de route
  - Nombre de membres (militaires + civils)
  - Budget alloué
- **Transaction SQL** pour les objectifs de capture

### 3. Récapitulatif complet d'une mission

**Première partie (synthèse)**
- Dates (départ / retour prévu)
- Feuille de route et objectifs de capture
- Budget initial / budget restant
- Liste des membres + responsable

**Deuxième partie (journal & contacts)**
- Journal de bord (navigation sans accès BDD, via liaison de données)
- Gestion des informateurs (nom de code, espèce, date, somme, appréciation)
- Bilan des captures (tableau local : espèce, objectif, réalisé, taux)
- Intégration au **PDF de compte-rendu**

### 4. Visualisation des races répertoriées
- Parcours de toutes les races (alliées et ennemies)
- **Filtres** pour accès rapide
- Pour chaque race :
  - **Ennemie** : type d'arme, agressivité
  - **Alliée** : date de premier contact, bienveillance, instrument favori

### 5. Informations sur les planètes
- Données générales : atmosphère, pesanteur
- Peuplement : espèces présentes avec leur pourcentage
- Historique des missions sur chaque planète
- Toutes les planètes apparaissent (même sans mission)

### 6. Statistiques avancées
Affiche les statistices globales de la base SQLite:
- Missions communes
- Grosses missions
- Missions par planète
- Top dépenses
- Informateurs mal payés

### 7. Export PDF
- Généré depuis le tableau de bord
- Contient : dates, responsable, budget, feuille de route, membres, journal, dépenses, contacts, bilan des captures

---
Projet réalisé dans le cadre du département informatique de l'IUT Robert Schuman.
